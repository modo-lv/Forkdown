using System;
using System.IO;
using Forkdown.Core;
using Forkdown.Core.Elements;
using Forkdown.Core.Wiring;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Scriban;
using Scriban.Runtime;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;
using Document = Forkdown.Core.Elements.Document;
using Path = Fluent.IO.Path;

#pragma warning disable 1591

namespace Forkdown.Html.Main {
  public partial class HtmlBuilder {
    private readonly Project _project;
    private readonly JsBuilder _jsBuilder; 
    private readonly ILogger<HtmlBuilder> _logger;
    private readonly BuildArguments _args;
    private readonly Path _inPath;
    private readonly Path _outPath;
    public HtmlBuilder(Project project, ILogger<HtmlBuilder> logger, BuildArguments args, JsBuilder jsBuilder) {
      _project = project;
      _logger = logger;
      _args = args;
      _jsBuilder = jsBuilder;
      _inPath = Program.InPath.Combine("html");
      _outPath = args.ProjectRoot.Combine(Program.OutFolder);
    }


    public HtmlBuilder Build(HtmlConfig? config = null) {
      config ??= new HtmlConfig();
      _project.Load();
      this._logger.LogInformation("Building {output} in {root}...", "HTML", _outPath.ToString());
      
      Document? mainMenu = null;
      { // Main menu
        var mmFile = _args.ProjectRoot.Combine("layout/main_menu.md");
        if (mmFile.Exists) {
          mainMenu = _project.LoadFile(mmFile);
          _project.ProcessLinks(mainMenu);
        }
      }
      Document? footer = null;
      { // Footer
        var fFile = _args.ProjectRoot.Combine("layout/footer.md");
        if (fFile.Exists) {
          footer = _project.LoadFile(fFile);
          _project.ProcessLinks(footer);
        }
      }

      var inFile = _inPath.Combine("page.scriban-html").FullPath;
      var templateContext = new TemplateContext
      {
        TemplateLoader = new ScribanTemplateLoader(_inPath),
        MemberRenamer = _ => _.Name
      };

      var model = new ScriptObject
      {
        { "Project", this._project },
        { "HtmlConfigJson", JsonConvert.SerializeObject(config) },
        { "Scripts", _jsBuilder.ScriptPaths },
        { "Timestamp", DateTime.Now.Ticks },
      };
      templateContext.PushGlobal(model);
      var template = Template.Parse(File.ReadAllText(inFile));

      // pages
      _outPath.CreateDirectories();
      if (mainMenu != null) {
        this.ProcessLinks(mainMenu);
        ProcessClasses(mainMenu);
      }
      if (footer != null) {
        this.ProcessLinks(footer);
        ProcessClasses(footer);
      }
      foreach (Document doc in this._project.Pages)
      {
        this.ProcessLinks(doc);
        ProcessClasses(doc);

        var outFile = doc.ProjectFilePath.TrimSuffix(".md") + ".html";
        Path outPath = _outPath.Combine(outFile);
        outPath.Parent().CreateDirectories();
        this._logger.LogDebug("Rendering {page}...", outFile);

        model.Add("Document", doc);
        model.Add("PathToRoot", "../".Repeat(doc.Depth).TrimSuffix("/"));
        model.Add("MainMenu", mainMenu);
        model.Add("Footer", footer);
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
        var target = Globals.Id(link.Target);
        if (index.ContainsKey(target))
          link.Target = $"{index[target].ProjectFilePath.TrimSuffix(".md")}.html#{target}";
      }
      else
      {
        el.Subs.ForEach(_ => this.ProcessLinks(_, doc));
      }
    }
  }
}
