using System.IO;
using System.Linq;
using System.Text;
using dotless.Core;
using Forkdown.Core.Wiring;
using Microsoft.Extensions.Logging;
using Simpler.NetCore.Collections;
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
    public CssBuilder Build() {
      _logger.LogInformation("Building {css}...", "CSS");
      var sb = new StringBuilder();
      _inPath.Files("*.less", true).OrderBy(_ => _.ToString()).ForEach(file => {
        _logger.LogDebug("Parsing {file}...", file.MakeRelativeTo(_inPath).ToString());
        var css = Less.Parse(File.ReadAllText(file.ToString()));
        sb.AppendLine(css);
      });
      _logger.LogDebug("Writing {file}...", _outFile.MakeRelativeTo(_outFile.Parent()).ToString());
      File.WriteAllText(_outFile.ToString(), sb.ToString());
      return this;
    }
  }
}
