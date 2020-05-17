using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Forkdown.Core.Main.Elements;
using Forkdown.Core.Main.Parsing;
using Forkdown.Core.Wiring;
using Microsoft.Extensions.Logging;
using Simpler.NetCore.Text;
using Path = Fluent.IO.Path;

namespace Forkdown.Core.Main {
  public class Project {
    /// <summary>
    /// Directory containing the project files.
    /// </summary>
    public readonly Path? Root;

    public ProjectConfig.ProjectConfig Config = new ProjectConfig.ProjectConfig();

    public ISet<Document> Pages = new HashSet<Document>();

    public String Name
    {
      get => this.Config.Name;
      set => this.Config.Name = value;
    }


    private readonly ILogger<Project> _logger;

    public Project(ILogger<Project> logger, AppArguments args) {
      this._logger = logger;
      this.Root = Path.Get(args.ProjectRoot);
    }


    public Project Load() {
      if (!this.Root?.Exists ?? false)
        throw new DirectoryNotFoundException($"Project location not found: `{this.Root}`");
      this._logger.LogInformation("Loading project from {dir}...", this.Root!.ToString());

      // Settings
      this.Config = ProjectConfig.ProjectConfig.FromYaml(this.Root!);
      this.Config.Name = this.Config.Name.NonBlank() ?? this.Root!.FileName;

      // Pages
      this._logger.LogInformation("Loading pages...");
      this.Pages = this.Root!.Combine("pages")
        .Files("*.md", true)
        .Select(doc => {
          var relative = doc.MakeRelativeTo(this.Root!);
          _Logger.LogDebug("Loading {doc}...", relative.ToString());
          return ForkdownConvert.ToDocument(
            doc,
            relative.ToString().TrimSuffix(".md", StringComparison.InvariantCultureIgnoreCase)
          );
        })
        .ToHashSet();

      this._logger.LogInformation("Project \"{name}\" loaded, {pages} page(s).", this.Config.Name, this.Pages.Count);
      return this;
    }


    private static ILogger _Logger = Program.Logger<Project>();
  }
}