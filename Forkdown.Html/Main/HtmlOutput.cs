using System.IO;
using Forkdown.Core;
using Forkdown.Core.Elements;
using Forkdown.Core.Wiring;
using Microsoft.Extensions.Logging;
using Scriban;
using Scriban.Runtime;
using Path = Fluent.IO.Path;

#pragma warning disable 1591

namespace Forkdown.Html.Main {
  public class HtmlOutput {
    /// <summary>
    /// Location of the HTML output.
    /// </summary>
    public Path OutRoot { get; protected set; }

    /// <summary>
    /// Forkdown project to build from.
    /// </summary>
    public readonly Project Project;


    private readonly ILogger<HtmlOutput> _logger;
    private readonly BuildArguments _args;
    public HtmlOutput(Project project, ILogger<HtmlOutput> logger, BuildArguments args) {
      this.Project = project;
      this._logger = logger;
      this._args = args;
    }


    public HtmlOutput BuildHtml() {
      this.Project.Load();
      
      this.OutRoot = this._args.ProjectRoot.CreateSubDirectory("out-net");

      this._logger.LogInformation("Building {output} in {root}...", "HTML", this.OutRoot!.ToString());

      var inRoot = Path.Get("Resources/Output/Default/Html");
      var inFile = inRoot.Combine("page.scriban-html").FullPath;
      var templateContext = new TemplateContext
      {
        TemplateLoader = new ScribanTemplateLoader(inRoot),
        MemberRenamer = _ => _.Name
      };
      var model = new ScriptObject
      {
        { "Project", this.Project },
      };
      templateContext.PushGlobal(model);
      var template = Template.Parse(text: File.ReadAllText(inFile));

      foreach (Document doc in this.Project.Pages)
      {
        var outFile = doc.FileName + ".html";
        Path outPath = this.OutRoot.Combine(outFile);
        outPath.Parent().CreateDirectories();
        this._logger.LogDebug("Rendering {page}...", outFile);
        
        model.Add("Document", doc);
        var html = template.Render(templateContext);
        File.WriteAllText(outPath.ToString(), html);
      }

      return this;
    }
  }
}