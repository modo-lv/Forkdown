using System.Linq;
using Forkdown.Core;
using Microsoft.Extensions.Logging;

namespace Forkdown.Html.Main {
  /// <summary>
  /// Main container and build runner for the HTML output of a Forkdown project.
  /// </summary>
  public class HtmlProject {
    private readonly Project _project;
    private readonly JsBuilder _jsBuilder;
    private readonly CssBuilder _cssBuilder;
    private readonly HtmlBuilder _htmlBuilder;

    /// <inheritdoc cref="HtmlProject" />
    public HtmlProject(Project project, ILogger<HtmlProject> logger, JsBuilder jsBuilder, CssBuilder cssBuilder,
      HtmlBuilder htmlBuilder) {
      _project = project;
      _jsBuilder = jsBuilder;
      _cssBuilder = cssBuilder;
      _htmlBuilder = htmlBuilder;

    }

    /// <summary>
    /// Load project and build the whole HTML+CSS+JS output. 
    /// </summary>
    public HtmlProject LoadAndBuildEverything() {
      _project.BuildAllPages();

      var layout = new[] { "main_menu", "footer" }.ToDictionary(_ => _, _ => {
        var file = _project.PathTo($"layout/{_}.md");
        return file.Exists ? _project.Build(file) : null;
      });

      _htmlBuilder.Build(layout);
      _jsBuilder.Build();
      _cssBuilder.Build();

      return this;
    }
  }
}
