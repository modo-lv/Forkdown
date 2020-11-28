using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Forkdown.Core;
using Forkdown.Core.Elements;
using Microsoft.Extensions.Logging;
using Scriban;
using Scriban.Runtime;
using Simpler.NetCore.Text;
using Path = Fluent.IO.Path;

namespace Forkdown.Html.Main {
  /// <summary>
  /// Builder for the HTML pages for this project.
  /// </summary>
  public class HtmlBuilder {

    private readonly Project _project;
    private readonly Path _inPath;
    private readonly ILogger<HtmlBuilder> _logger;
    private readonly JsBuilder _jsBuilder;
    private readonly ProjectPath _outPath;

    /// <inheritdoc cref="HtmlBuilder"/>
    public HtmlBuilder(Project project, ILogger<HtmlBuilder> logger, JsBuilder jsBuilder) {
      _project = project;
      _logger = logger;
      _jsBuilder = jsBuilder;

      _inPath = Program.InPath.Combine("scriban");
      _outPath = _project.PathTo(Program.OutFolder);
    }

    /// <summary>
    /// Load Scriban templates and build HTML.
    /// </summary>
    /// <param name="layout">Header, footer and other layout fragments.</param>
    public HtmlBuilder Build(IDictionary<String, Document?> layout) {
      _logger.LogInformation($"Building {{h}} in {_project.PathTo(".").FullPathString()}/{{path}}...", "HTML",
        _outPath);

      var start = DateTime.Now;
      var inFile = _inPath.Combine("page.scriban-html").FullPath;
      var templateContext = new TemplateContext {
        TemplateLoader = new ScribanTemplateLoader(_inPath),
        MemberRenamer = _ => _.Name
      };

      var json = new ScriptObject();
      json.Import(typeof(Json), null, member => member.Name);

      var model = new ScriptObject {
        { "Project", this._project },
//        { "HtmlConfigJson", JsonConvert.SerializeObject(config) },
        { "Scripts", _jsBuilder.ScriptPaths },
        { "Timestamp", DateTime.Now.Ticks },
        { "Json", json },
        { "MainMenu", layout["main_menu"] },
        { "Footer", layout["footer"] },
      };
      templateContext.PushGlobal(model);
      var template = Template.Parse(File.ReadAllText(inFile));

      var docs = this._project.Pages.Append(
        new Document { ProjectFilePath = "components/settings.md" }
      );

      foreach (Document doc in docs) {
        var outFile = doc.ProjectFilePath.TrimSuffix(".md") + ".html";
        Path outPath = _outPath.Combine(outFile);
        outPath.Parent().CreateDirectories();
        this._logger.LogDebug("Rendering {page}...", outFile);

        model["ComponentName"] = doc.ProjectFilePath.StartsWith("components/")
          ? outFile.TrimPrefix("components/").TrimSuffix(".html")
          : "";
        model["Document"] = doc;
        model["PathToRoot"] = "../".Repeat(doc.Depth).TrimSuffix("/");
        var html = template.Render(templateContext);
        File.WriteAllText(outPath.ToString(), html);
      }

      var elapsed = (DateTime.Now - start).TotalSeconds;
      _logger.LogInformation(
        "{pages} HTML page(s), including layout files, built in {s:0.00} seconds.",
        this._project.Pages.Count + layout.Count, elapsed
      );

      return this;

    }
  }
}
