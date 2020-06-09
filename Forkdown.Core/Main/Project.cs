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
    public ISet<Document> Pages = Nil.S<Document>();

    /// <summary>
    /// Index mapping anchors to the pages they are in. 
    /// </summary>
    public IDictionary<String, Document> InteralLinks = Nil.DStr<Document>();


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
    /// Load the project, including processing all the Forkdown pages. 
    /// </summary>
    /// <exception cref="DirectoryNotFoundException">
    /// If the project directory doesn't exist or isn't a directory.
    /// </exception>
    public Project Load() {
      var root = _args.ProjectRoot;

      if (!root.Exists)
        throw new DirectoryNotFoundException($"Project location not found: {root}");
      if (!root.IsDirectory)
        throw new DirectoryNotFoundException($"Project location is not a directory: {root}");
      _logger.LogInformation("Loading project from {dir}...", root.ToString());

      // Settings
      this.Config = BuildConfig.From(_args.MainConfigFile);
      this.Config.Name = this.Config.Name.NonBlank() ?? root.FileName;

      // Builder
      // Pages
      var start = DateTime.Now.Millisecond;
      _logger.LogInformation("Finding and loading pages...");
      this.Pages = root.Combine("pages")
        .Files("*.md", true)
        .Select(doc => this.LoadFile(this.PathTo(doc)))
        .ToHashSet();
      var delta = ((Decimal)DateTime.Now.Millisecond - start) / 1000;

      // Achors
      this.InteralLinks = _builder.Storage.Get<LinkIndex>();


      _logger.LogInformation(
        "Project \"{name}\" with {pages} page(s) loaded in {s:0.00} seconds.",
        this.Config.Name, this.Pages.Count, delta
      );
      return this;
    }

    public Document LoadFile(Path file) => this.LoadFile(this.PathTo(file));
    public Document LoadFile(ProjectPath file) {
      _logger.LogDebug("Loading {doc}...", file.RelPathString());
      return _builder.Build(File.ReadAllText(file.FullPathString()), this.Config, file);
    }

    public ProjectPath PathTo(Path file) =>
      new ProjectPath(_args.ProjectRoot, file);
  }
}
