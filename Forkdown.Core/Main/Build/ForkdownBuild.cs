using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Config;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Build; 

public partial class ForkdownBuild {


  public readonly ISet<Type> Workers = Nil.S<Type>();
  public BuildContext Context = new BuildContext();
  public WorkerStorage Storage => this.Context.Storage;


  public Document Run(String markdown, ProjectPath? file = null) =>
    this.Run(FromMarkdown.ToForkdown(markdown, file));

  public IEnumerable<Document> Run(IEnumerable<Document> documents) {
    if (this.Context == null)
      throw new NullReferenceException("Can't build without a build context.");
    var builders = new BuilderQueue(this.Workers, this.Context);
    var ran = Nil.S<Type>();
      
    return builders.Aggregate(documents, (docs, builder) => 
      docs.Select(doc => (Document)BuildElement(doc, builder, ran)).ToList()
    );
  }

  public Document Run(Document doc) => this.Run(new[] { doc }).First();


  protected static Element BuildElement(Element element, Builder builder, ISet<Type> ran)
  {

    if (builder.MustRunAfter.Any() && !builder.MustRunAfter.Overlaps(ran)) {
      var missing = builder.MustRunAfter.Except(ran).Select(_ => _.Name);
      throw new Exception($"Cannot run {builder.Type.Name}: must run {String.Join(", ", missing)} first!");
    }
    try {
      //Console.WriteLine($"Running {builder.Type} on {(element as Document)!.ProjectFileId}");
      return builder.Build(element);
    }
    finally {
      ran.Add(builder.Type);
    }
  }

  /// <summary>
  /// Set the configuration for this build.
  /// </summary>
  public ForkdownBuild WithConfig(MainConfig config) {
    this.Context.Config = config;
    return this;
  }

  /// <summary>
  /// Set the build context for this build.
  /// </summary>
  public ForkdownBuild SetContext(BuildContext context) {
    this.Context = context;
    return this;
  }

  /// <summary>
  /// Add a new <see cref="Worker"/> to the set of workers for this build.
  /// </summary>
  public ForkdownBuild AddWorker<TBuilder>() where TBuilder : Worker {
    this.Workers.Add(typeof(TBuilder));
    return this;
  }
}