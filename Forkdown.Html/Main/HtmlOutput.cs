using System;
using System.IO;
using System.Linq;
using Forkdown.Core.Main;
using Forkdown.Core.Main.Elements;
using Microsoft.Extensions.Logging;
using Scriban;
using Scriban.Parsing;
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

    public HtmlOutput(Project project, ILogger<HtmlOutput> logger) {
      this.Project = project;
      this._logger = logger;
    }


    public HtmlOutput Build() {
      this.OutRoot = this.Project.Root!.CreateSubDirectory("out-net");

      this._logger.LogInformation("Building {output} in {root}...", "HTML", this.OutRoot!.ToString());

      var inRoot = Path.Get("Resources/Output/Default/Html");
      var inFile = inRoot.Combine("page.scriban-html").FullPath;
      var templateContext = new TemplateContext
      {
        TemplateLoader = new TemplateLoader(inRoot),
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
        model.Add("Document", doc);
        Console.WriteLine(template.Render(templateContext));
        break;
      }

      return this;
    }
  }
}