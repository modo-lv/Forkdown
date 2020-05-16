using System.IO;
using Forkdown.Core.Main;
using Microsoft.Extensions.Logging;

#pragma warning disable 1591

namespace Forkdown.Html.Main {
  public class HtmlOutput {
    /// <summary>
    /// Location of the HTML output.
    /// </summary>
    public readonly DirectoryInfo Root;
    
    /// <summary>
    /// Forkdown project to build from.
    /// </summary>
    public readonly Project Project;

    
    private readonly ILogger<HtmlOutput> _logger;
    public HtmlOutput(Project project, ILogger<HtmlOutput> logger) {
      this.Project = project;
      this._logger = logger;
      
      this.Root = project.Root.CreateSubdirectory("out-net");
    }


    
    public HtmlOutput Build() {
      this.Project.Load();
      
      this._logger.LogInformation("Building {output} in {root}...", "HTML", this.Root);
      
      return this;
    }
  }
}