using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Parsing.Forkdown.Processors;
using Simpler.NetCore.Collections;
using Path = Fluent.IO.Path;

namespace Forkdown.Core.Parsing.Forkdown {
  public partial class ForkdownBuilder {
    public readonly IList<IForkdownProcessor> Chain = Nil.L<IForkdownProcessor>();
    private Boolean _isDone;

    public Document Build(Path file) =>
      this.Build(File.ReadAllText(file.FullPath), file);

    public Document Build(String markdown, Path? file = null) {
      var mDoc = MarkdownBuilder.DefaultBuild(markdown);
      var fDoc = (Document) FromMarkdown.ToForkdown(mDoc);
      fDoc.ProjectFilePath = file?.ToString() ?? "";
      return this.Build(fDoc);
    }

    public Document Build(Document fDoc) {
      if (this._isDone)
        throw new Exception("Don't reuse builders!");
      this._isDone = true;
      
      return process(fDoc, Nil.DStr<Object>());

      T process<T>(T element, IDictionary<String, Object> context) where T : Element {
        element = this.Chain.Aggregate(element, (e, p) => p.Process(e, context));
        element.Subs = element.Subs.Select(e => process(e, new Dictionary<String, Object>(context))).ToList();
        return element;
      }
    }


    public ForkdownBuilder AddProcessor<T>() where T : IForkdownProcessor, new() {
      this.Chain.Add(new T());
      return this;
    }
  }
}
