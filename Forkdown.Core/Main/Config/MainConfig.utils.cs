using System.IO;
using Microsoft.Extensions.Logging;
using Simpler.NetCore.Text;
using Yaml = YamlDotNet.Serialization;

namespace Forkdown.Core.Config {
  public partial class MainConfig {
    private static readonly ILogger<MainConfig> _logger = Program.Logger<MainConfig>();

    public static MainConfig From(MainConfigSource source) {
      return new MainConfig
      {
        Name = source["name"].ToString() ?? "",
        ExternalLinks = ExternalLinkConfig.From(source)
      };
    }


    /// <summary>
    /// Load a project's main settings file.
    /// </summary>
    /// <param name="file">File to read settings from.</param>
    /// <returns>Default project settings selectively overridden by values from the file.</returns>
    public static MainConfig From(FileInfo file) {
      var result = new MainConfig();
      if (!file.Exists)
        _logger.LogWarning("Settings file {file} not found!", file.Name);
      else
      {
        _logger.LogInformation("Loading settings from {file}...", file.Name);
        result = From(MainConfigSource.From(file));
      }

      if (result.Name.IsBlank())
      {
        result.Name = file.DirectoryName;
        _logger.LogWarning(
          "Project name not found in settings; using unreliable folder name \"{name}\"!", result.Name);
      }

      return result;
    }
  }
}
