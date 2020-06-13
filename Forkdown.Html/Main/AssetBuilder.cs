using System.IO;
using Forkdown.Core;
using Microsoft.Extensions.Logging;

namespace Forkdown.Html.Main {
  /// <summary>
  /// Copies built-in and project assets to the output.
  /// </summary>
  public class AssetBuilder {
    private readonly ProjectPath _outPath;
    private readonly ILogger<AssetBuilder> _logger;

    /// <inheritdoc cref="AssetBuilder"/>
    public AssetBuilder(Project project, ILogger<AssetBuilder> logger) {
      _logger = logger;
      _outPath = project.PathTo($"{Program.OutFolder}/assets");
    }
    
    /// <inheritdoc cref="AssetBuilder"/>
    public void Build() {
      _logger.LogInformation("Copying {s}...", "assets");
      var builtinSource = Program.InPath.Combine("assets");
      builtinSource.AllFiles().ForEach(file => {
        var outFile = _outPath.Combine(file.MakeRelativeTo(builtinSource).ToString());
        _logger.LogDebug("Copying {source} to {target}...", file.FullPath, outFile.FullPath);
        outFile.Parent().CreateDirectories();
        File.Copy(file.FullPath, outFile.FullPath, overwrite: true);
      });
    }
  }
}
