using System.IO;
using Forkdown.Core;
using Forkdown.Core.Wiring;
using Microsoft.Extensions.Logging;
using SharpScss;
using Path = Fluent.IO.Path;

namespace Forkdown.Html.Main; 

/// <summary>
/// Compile SASS/SCSS files to CSS.
/// </summary>
public class SassBuilder {
  private readonly ILogger<SassBuilder> _logger;
  private readonly Path _inPath;
  private readonly Path _outFile;


  /// <inheritdoc cref="SassBuilder"/>
  public SassBuilder(BuildArguments args, ILogger<SassBuilder> logger) {
    _logger = logger;
    _inPath = Program.InPath.Combine("sass");
    _outFile = args.ProjectRoot.Combine(Program.OutFolder, "main.css");
  }

  /// <summary>
  /// Compile built-in and project's SASS files into CSS
  /// </summary>
  public SassBuilder Build() {
    _logger.LogInformation("Compiling {css}...", "SASS");

    var file = _inPath.Combine($"{Globals.Prefix}main.sass");

    if (!file.Exists) {
      _logger.LogError("LESS file {file} not found!", file.FullPath);
      return this;
    }

    var result = Scss.ConvertFileToCss(file.FullPath, new ScssOptions {
      IsIndentedSyntaxSource = true,
    });
    File.WriteAllText(_outFile.ToString(), result.Css);

    return this;
  }
}