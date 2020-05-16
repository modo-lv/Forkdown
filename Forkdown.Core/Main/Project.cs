using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Forkdown.Core.Internal;
using Forkdown.Core.Main.Elements;
using Forkdown.Core.Main.Parsing;
using Forkdown.Core.Wiring;
using Microsoft.Extensions.Logging;

namespace Forkdown.Core.Main {
  public partial class Project {
    /// <summary>
    /// Directory containing the project files.
    /// </summary>
    public readonly DirectoryInfo? Root;

    public ProjectConfig Config = new ProjectConfig();

    public ISet<Document> Pages = new HashSet<Document>();


    private readonly ILogger<Project> _logger;

    public Project(ILogger<Project> logger, AppArguments args) {
      this._logger = logger;
      this.Root = new DirectoryInfo(args.ProjectRoot);
    }


    public Project Load() {
      if (!this.Root?.Exists ?? false)
        throw new DirectoryNotFoundException($"Project location not found: `{this.Root}`");

      this._logger.LogInformation("Loading project from {dir}...", this.Root);

      this.Config = ProjectConfig.FromYaml(this.Root!);
      this.Config.Name = this.Config.Name.NonBlank() ?? this.Root!.Name;

      this.LoadPages();

      this._logger.LogInformation("Project \"{name}\" loaded.", this.Config.Name);

      return this;
    }

    public Project LoadPages() {
      this._logger.LogInformation("Loading pages...");

      this.Pages = LoadPages(this.Root!);

      return this;
    }


    private static ILogger _Logger = Program.Logger<Project>();

    private static ISet<Document> LoadPages(DirectoryInfo dir) {
      var result = dir.EnumerateFiles("*.md")
        .Select(doc => {
          _Logger.LogDebug("Loading {doc}...", doc);
          return ForkdownObject.ToDocument(doc);
        })
        .ToHashSet();
      
      foreach (var d in dir.EnumerateDirectories())
      foreach (var p in LoadPages(d))
        result.Add(p);

      return result;
    }
  }
}