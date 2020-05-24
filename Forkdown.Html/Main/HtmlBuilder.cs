using System.IO;
using Forkdown.Core;
using Forkdown.Core.Elements;
using Forkdown.Core.Parsing.Forkdown;
using Forkdown.Core.Wiring;
using Microsoft.Extensions.Logging;
using Scriban;
using Scriban.Runtime;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;
using Document = Forkdown.Core.Elements.Document;
using Path = Fluent.IO.Path;

#pragma warning disable 1591

namespace Forkdown.Html.Main {
  public partial class HtmlBuilder {
    /// <summary>
    /// Forkdown project to build from.
    /// </summary>
    private readonly Project _project;
    private readonly JsBuilder _jsBuilder; 
    private readonly ILogger<HtmlBuilder> _logger;
    private readonly BuildArguments _args;
    public HtmlBuilder(Project project, ILogger<HtmlBuilder> logger, BuildArguments args, JsBuilder jsBuilder) {
      _project = project;
      _logger = logger;
      _args = args;
      _jsBuilder = jsBuilder;
    }


    public HtmlBuilder Build() {
      _project.Load();
      var outRoot = this._args.ProjectRoot.Combine("out-net").CreateDirectories();

      this._logger.LogInformation("Building {output} in {root}...", "HTML", outRoot.ToString());

      var inRoot = Path.Get("Resources/Output/Html");
      var inFile = inRoot.Combine("page.scriban-html").FullPath;
      var templateContext = new TemplateContext
      {
        TemplateLoader = new ScribanTemplateLoader(inRoot),
        MemberRenamer = _ => _.Name
      };
      var model = new ScriptObject
      {
        { "Project", this._project },
        { "Scripts", _jsBuilder.ScriptPaths },
      };
      templateContext.PushGlobal(model);
      var template = Template.Parse(text: File.ReadAllText(inFile));

      foreach (Document doc in this._project.Pages)
      {
        this.ProcessLinks(doc);
        ProcessClasses(doc);

        var outFile = doc.FileName + ".html";
        Path outPath = outRoot.Combine("pages", outFile);
        outPath.Parent().CreateDirectories();
        this._logger.LogDebug("Rendering {page}...", outFile);

        model.Add("Document", doc);
        model.Add("PathToRoot", "../".Repeat(doc.Depth + 1));
        var html = template.Render(templateContext);
        File.WriteAllText(outPath.ToString(), html);
      }

      return this;
    }

    public void ProcessLinks(Element el, Document? doc = null) {
      doc ??= (Document) el;
      var index = this._project.InteralLinks;
      if (el is Link link && link.IsInternal)
      {
        var target = GlobalId.From(link.Target);
        if (index.ContainsKey(target))
          link.Target = $"{"../".Repeat(doc.Depth)}{index[target].FileName}.html#{target}";
      }
      else
      {
        el.Subs.ForEach(_ => this.ProcessLinks(_, doc));
      }
    }
  }
}
