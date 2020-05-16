using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Forkdown.Core.Main {
  public class Project {
    /// <summary>
    /// Directory containing the project files.
    /// </summary>
    public DirectoryInfo? Dir;

    /// <summary>
    /// Pretty name for the project, taken from project config. 
    /// </summary>
    public String? Title;

    
    private readonly ILogger<Project> _logger;
    public Project(ILogger<Project> logger) { this._logger = logger; }

    
    public Project Build() {
      if (!this.Dir?.Exists ?? false)
        throw new DirectoryNotFoundException($"Project directory not found: `{this.Dir}`");

      this.Title = this.Dir?.Name;

      this._logger.LogInformation("Building \"{title}\" in {dir}...", this.Title, this.Dir);

      return this;
    }
  }
}