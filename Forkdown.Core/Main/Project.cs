using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Forkdown.Core.Config;
using Forkdown.Core.Elements;
using Forkdown.Core.Parsing.Forkdown;
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
    /// <inheritdoc cref="MainConfig"/>
    public MainConfig Config = new MainConfig();

    /// <inheritdoc cref="MainConfig.Name"/>
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
    private readonly ForkdownBuilder _builder;
    public Project(ILogger<Project> logger, BuildArguments args, ForkdownBuilder builder) {
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
      this.Config = MainConfig.From(_args.MainConfigFile);
      this.Config.Name = this.Config.Name.NonBlank() ?? root.FileName;

      // Builder
      // Pages
      _logger.LogInformation("Finding and loading pages...");
      this.Pages = root.Combine("pages")
        .Files("*.md", true)
        .Select(doc => this.LoadFile(this.PathTo(doc)))
        .ToHashSet();

      // Achors
      this._logger.LogDebug("Building internal link index...");
      this.InteralLinks = InternalLinks.From(this.Pages);

      // Links
      this._logger.LogDebug("Updating internal link targets...");
      this.Pages.ForEach(this.ProcessLinks);

      _logger.LogInformation("Project \"{name}\" loaded, {pages} page(s).", this.Config.Name, this.Pages.Count);
      return this;
    }

    public void ProcessLinks(Element el) {
      if (el is Link link && (link.IsExternal || !this.InteralLinks.ContainsKey(link.Target))) {
        if (link.Target.StartsWith("@"))
          link.Target = link.Target.Part(1);
        link.Target = this.Config.ExternalLinks.UrlFor(link.Target);
      }
      else
        el.Subs.ForEach(this.ProcessLinks);
    }

    public Document LoadFile(Path file) => this.LoadFile(this.PathTo(file));
    public Document LoadFile(ProjectPath file) {
      _logger.LogDebug("Loading {doc}...", file.RelPathString());
      return _builder.Build(File.ReadAllText(file.FullPathString()), file);
    }

    public ProjectPath PathTo(Path file) => 
      new ProjectPath(_args.ProjectRoot, file);
  }
}
