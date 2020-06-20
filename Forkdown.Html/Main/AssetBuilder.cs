using System;
using System.IO;
using Forkdown.Core;
using Microsoft.Extensions.Logging;
using Simpler.NetCore.Collections;

namespace Forkdown.Html.Main {
  /// <summary>
  /// Copies assets, libraries and any other files directly from resources to output.  
  /// </summary>
  public class AssetBuilder {
    private readonly ProjectPath _outPath;
    private readonly ILogger<AssetBuilder> _logger;

    /// <inheritdoc cref="AssetBuilder"/>
    public AssetBuilder(Project project, ILogger<AssetBuilder> logger) {
      _logger = logger;
      _outPath = project.PathTo($"{Program.OutFolder}");
    }

    /// <inheritdoc cref="AssetBuilder"/>
    public void Build() => this.Build("assets", "libs");

    /// <inheritdoc cref="AssetBuilder"/>
    public void Build(params String[] folders) {
      folders.ForEach(folder => {
        _logger.LogInformation("Copying {s}...", folder);
        var builtinSource = Program.InPath.Combine(folder);
        builtinSource.AllFiles().ForEach(file => {
          var outFile = _outPath.Combine(folder).Combine(file.MakeRelativeTo(builtinSource).ToString());
          _logger.LogDebug("Copying {source} to {target}...", file.FullPath, outFile.FullPath);
          outFile.Parent().CreateDirectories();
          File.Copy(file.FullPath, outFile.FullPath, overwrite: true);
        });
      });
    }
  }
}
