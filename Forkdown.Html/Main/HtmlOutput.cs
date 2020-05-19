using System;
using System.IO;
using System.Linq;
using Forkdown.Core;
using Forkdown.Core.Elements;
using Forkdown.Core.Parsing;
using Forkdown.Core.Wiring;
using Microsoft.Extensions.Logging;
using Scriban;
using Scriban.Runtime;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;
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

      this.OutRoot = this._args.ProjectRoot.Combine("out-net");
    }


    public HtmlOutput BuildHtml() {
      this.Project.Load();
      this.OutRoot.CreateDirectories();

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
        // Anchors
        this.AnchorLinks(doc);

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


    public void AnchorLinks(Element el, Document? doc = null) {
      doc ??= (Document) el;
      var index = this.Project.Anchors;
      if (el is Link link)
      {
        var target = AnchorIndex.Anchor(link.Target);
        if (link.IsInternal && index.ContainsKey(target))
          link.Target = $"{"../".Repeat(doc.Depth)}{index[target].FileName}.html#{target}";
      }
      else
      {
        el.Subs.ForEach(_ => this.AnchorLinks(_, doc));
      }
    }
  }
}
