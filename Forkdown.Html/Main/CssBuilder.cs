using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using dotless.Core;
using dotless.Core.configuration;
using Forkdown.Core.Wiring;
using Microsoft.Extensions.Logging;
using Path = Fluent.IO.Path;

namespace Forkdown.Html.Main {
  /// <summary>
  /// Builder for converting LESS to CSS.
  /// </summary>
  public partial class CssBuilder {
    private readonly Path _inPath;
    private readonly Path _outFile;
    private readonly ILogger<CssBuilder> _logger;
    /// <inheritdoc cref="CssBuilder"/>
    public CssBuilder(BuildArguments args, ILogger<CssBuilder> logger) {
      _logger = logger;
      _inPath = Program.InPath.Combine("less");
      _outFile = args.ProjectRoot.Combine(Program.OutFolder, "main.css");
    }



    /// <summary>
    /// Read all .less files from the Forkdown-HTML resources and convert them into main.css for output.
    /// </summary>
    /// <param name="config"></param>
    public CssBuilder Build(HtmlConfig? config = null) {
      _logger.LogInformation("Building {css}...", "CSS");

      config ??= new HtmlConfig();
      
      var file = _inPath.Combine("main.less");

      if (!file.Exists) {
        _logger.LogError("LESS file {file} not found!", file.FullPath);
        return this;
      }

      var sb = new StringBuilder()
          .AppendLine(File.ReadAllText(file.ToString()))
          .AppendLine($"@fd--min-article-width: {config.MinArticleWidth};");
      var less = Regex.Replace(sb.ToString(),
        @"(@import\s['""])\./",
        $"$1{file.Parent().FullPath + System.IO.Path.DirectorySeparatorChar}",
        RegexOptions.IgnoreCase & RegexOptions.Multiline
      );
      
      var css = Less.Parse(less, new DotlessConfiguration { DisableUrlRewriting = true });
      // the `DisableUrlRewriting` doesn't work, so we have to revert the URLs manually
      css = css.Replace($"{_inPath.FullPath.Replace("\\", "/")}/", "");
      // Comments also aren't stripped and get confused for URLs sometimes >:(
      css = Regex.Replace(css, "/\\*.+\\*/", "");

      _logger.LogDebug("Writing {file}...", _outFile.MakeRelativeTo(_outFile.Parent()).ToString());
      File.WriteAllText(_outFile.ToString(), css);
      return this;
    }
  }
}
