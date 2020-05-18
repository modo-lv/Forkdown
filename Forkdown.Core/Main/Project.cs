using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Forkdown.Core.Config;
using Forkdown.Core.Elements;
using Forkdown.Core.Wiring;
using Microsoft.Extensions.Logging;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;
using Path = Fluent.IO.Path;

namespace Forkdown.Core {
  public class Project {
    /// <summary>
    /// Main project location.
    /// </summary>
    public readonly Path Root;

    /// <inheritdoc cref="MainConfig"/>
    public MainConfig Config = new MainConfig();

    /// <summary>
    /// Project's Forkdown documents, parsed and processed.
    /// </summary>
    public ISet<Document> Pages = Nil.S<Document>();

    /// <inheritdoc cref="MainConfig.Name"/>
    public String Name => this.Config.Name;


    
    private readonly ILogger<Project> _logger;

    public Project(ILogger<Project> logger, AppArguments args) {
      this._logger = logger;

      this.Root = Path.Get(args.ProjectRoot);
    }

    
    /// <summary>
    /// Load the project, including processing all the Forkdown pages. 
    /// </summary>
    /// <exception cref="DirectoryNotFoundException">If <see cref="Root"/> is not an existing directory.</exception>
    public Project Load() {
      if (!this.Root.Exists)
        throw new DirectoryNotFoundException($"Project location not found: `{this.Root}`");

      this._logger.LogInformation("Loading project from {dir}...", this.Root.ToString());

      // Settings
      this.Config = MainConfig.FromYaml(this.Root!);
      this.Config.Name = this.Config.Name.NonBlank() ?? this.Root.FileName;

      // Pages
      this._logger.LogInformation("Loading pages...");
      this.Pages = this.Root.Combine("pages")
        .Files("*.md", true)
        .Select(doc => {
          var relative = doc.MakeRelativeTo(this.Root);
          this._logger.LogDebug("Loading {doc}...", relative.ToString());
          return Parsing.Forkdown.FromMarkdown(
            doc,
            relative.ToString().TrimSuffix(".md", StringComparison.InvariantCultureIgnoreCase)
          );
        })
        .ToHashSet();

      this._logger.LogInformation("Project \"{name}\" loaded, {pages} page(s).", this.Config.Name, this.Pages.Count);
      return this;
    }
  }
}
