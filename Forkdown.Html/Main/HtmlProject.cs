using System.Linq;
using Forkdown.Core;

namespace Forkdown.Html.Main; 

/// <summary>
/// Main container and build runner for the HTML output of a Forkdown project.
/// </summary>
public class HtmlProject {
  private readonly Project _project;
  private readonly JsBuilder _jsBuilder;
  private readonly HtmlBuilder _htmlBuilder;
  private readonly AssetBuilder _assetBuilder;
  private readonly SassBuilder _sassBuilder;

  /// <inheritdoc cref="HtmlProject" />
  public HtmlProject(Project project, JsBuilder jsBuilder,
    HtmlBuilder htmlBuilder, AssetBuilder assetBuilder, SassBuilder sassBuilder) {
    _project = project;
    _jsBuilder = jsBuilder;
    _htmlBuilder = htmlBuilder;
    _assetBuilder = assetBuilder;
    _sassBuilder = sassBuilder;
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

    _assetBuilder.Build();
    _htmlBuilder.Build(layout);
    _sassBuilder.Build();
    _jsBuilder.Build();
    //_cssBuilder.Build();

    return this;
  }
}