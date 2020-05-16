using System.IO;
using Forkdown.Core.Main;
using Microsoft.Extensions.Logging;

#pragma warning disable 1591

namespace Forkdown.Html.Main {
  public class HtmlOutput {
    public readonly DirectoryInfo Root;
    public readonly Project Project;

    private readonly ILogger<HtmlOutput> _logger;
    public HtmlOutput(Project project, ILogger<HtmlOutput> logger) {
      this.Project = project;
      this._logger = logger;
      
      this.Root = project.Root.CreateSubdirectory("out-net");
    }


    public HtmlOutput Build() {
      this.Project.Build();
      
      this._logger.LogInformation("Building {output} in {root}...", "web", this.Root);
      
      return this;
    }
  }
}