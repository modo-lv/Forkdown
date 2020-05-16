using System;
using System.IO;
using Forkdown.Core.Wiring;
using Microsoft.Extensions.Logging;

namespace Forkdown.Core.Main {
  public class Project {
    /// <summary>
    /// Directory containing the project files.
    /// </summary>
    public readonly DirectoryInfo Root;

    /// <summary>
    /// Pretty name for the project, taken from project config. 
    /// </summary>
    public String? Title;

    
    private readonly ILogger<Project> _logger;
    public Project(ILogger<Project> logger, AppArguments args) {
      this._logger = logger;
      this.Root = new DirectoryInfo(args.ProjectRoot);
    }

    
    public Project Build() {
      if (!this.Root?.Exists ?? false)
        throw new DirectoryNotFoundException($"Project location not found: `{this.Root}`");

      this.Title = this.Root?.Name;

      this._logger.LogInformation("Loading \"{title}\" from {dir}...", this.Title, this.Root);

      return this;
    }
  }
}