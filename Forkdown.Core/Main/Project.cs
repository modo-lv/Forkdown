using System;
using System.IO;
using Forkdown.Core.Internal;
using Forkdown.Core.Wiring;
using Microsoft.Extensions.Logging;
using Simpler.Net;

namespace Forkdown.Core.Main {
  public class Project {
    /// <summary>
    /// Directory containing the project files.
    /// </summary>
    public readonly DirectoryInfo? Root;

    public ProjectConfig Config = new ProjectConfig(); 

    private readonly ILogger<Project> _logger;
    public Project(ILogger<Project> logger, AppArguments args) {
      this._logger = logger;
      this.Root = new DirectoryInfo(args.ProjectRoot);
    }

    
    public Project Load() {
      if (!this.Root?.Exists ?? false)
        throw new DirectoryNotFoundException($"Project location not found: `{this.Root}`");
      
      this._logger.LogInformation("Loading project from {dir}...", this.Root);

      this.Config = ProjectConfig.FromYaml(this.Root!.File("forkdown.core.yaml"));
      this.Config.Name = this.Config.Name.NonBlank() ?? this.Root!.Name;
      
      this._logger.LogInformation("Project \"{name}\" loaded.", this.Config.Name);

      return this;
    }
  }
}