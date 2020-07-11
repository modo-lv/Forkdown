using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Forkdown.Core.Build;
using Forkdown.Core.Config;
using Forkdown.Core.Elements;
using Forkdown.Core.Wiring;
using Microsoft.Extensions.Logging;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;
using Path = Fluent.IO.Path;

// ReSharper disable UnusedMember.Global

namespace Forkdown.Core {
  /// <summary>
  /// Main Forkdown project class.
  /// </summary>
  public partial class Project {
    /// <inheritdoc cref="BuildConfig"/>
    public BuildConfig Config = new BuildConfig();

    /// <inheritdoc cref="BuildConfig.Name"/>
    public String Name => this.Config.Name;

    /// <summary>
    /// Project's Forkdown documents, parsed and processed.
    /// </summary>
    public IList<Document> Pages = Nil.L<Document>();

    /// <summary>
    /// Index mapping anchors to the pages they are in. 
    /// </summary>
    public IDictionary<String, Document> InternalLinks = Nil.DStr<Document>();
    
    public SinglesIndex SinglesIndex = new SinglesIndex();

    private Boolean _initialized;


    /// Constructor
    private readonly ILogger<Project> _logger;
    private readonly BuildArguments _args;
    private readonly MainBuilder _builder;
    public Project(ILogger<Project> logger, BuildArguments args, MainBuilder builder) {
      _logger = logger;
      _args = args;
      _builder = builder;
    }

    /// <summary>
    /// Initialize the project, including loading main configuration. 
    /// </summary>
    /// <exception cref="DirectoryNotFoundException">
    /// If the project directory doesn't exist or isn't a directory.
    /// </exception>
    public Project Init() {
      var root = _args.ProjectRoot;

      if (!root.Exists)
        throw new DirectoryNotFoundException($"Project location not found: {root}");
      if (!root.IsDirectory)
        throw new DirectoryNotFoundException($"Project location is not a directory: {root}");
      _logger.LogInformation("Loading project from {dir}...", root.ToString());

      // Settings
      this.Config = BuildConfig.From(_args.MainConfigFile);
      this.Config.Name = this.Config.Name.NonBlank() ?? root.FileName;
      _builder.Config = this.Config;

      _initialized = true;
      return this;
    }

    public Document Build(Path file) => this.Build(this.PathTo(file));
    public Document Build(ProjectPath file) {
      _logger.LogDebug("Loading {doc}...", file.RelPathString());
      return _builder.Build(File.ReadAllText(file.FullPathString()), file);
    }

    /// <summary>
    /// Process all the pages in the project into Forkdown documents, stored in <see cref="Pages"/>. 
    /// </summary>
    public Project BuildAllPages() {
      if (!_initialized)
        this.Init();

      var start = DateTime.Now;

      _logger.LogInformation("Finding and loading pages...");

      this.Pages = _builder.Build(
        this.PathTo("pages")
          .Files("*.md", true)
          .Select(FromMarkdown.ToForkdown)
      ).ToList();
      this.InternalLinks = _builder.Storage.Get<LinkIndex>();
      this.SinglesIndex = _builder.Storage.Get<SinglesIndex>();

      var elapsed = (DateTime.Now - start).TotalSeconds;
      _logger.LogInformation(
        "Project \"{name}\" with {pages} page(s) loaded in {s:0.00} seconds.",
        this.Config.Name, this.Pages.Count, elapsed
      );
      return this;
    }

    public ProjectPath PathTo(String file) =>
      this.PathTo(new Path(file));

    public ProjectPath PathTo(Path file) =>
      new ProjectPath(_args.ProjectRoot, file);
  }
}
