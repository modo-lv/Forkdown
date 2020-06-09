using System.IO;
using Microsoft.Extensions.Logging;
using Simpler.NetCore.Text;
using Yaml = YamlDotNet.Serialization;

namespace Forkdown.Core.Config {
  public partial class BuildConfig {
    private static readonly ILogger<BuildConfig> _logger = Program.Logger<BuildConfig>();

    public static BuildConfig From(ConfigSource source) {
      return new BuildConfig
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
    public static BuildConfig From(FileInfo file) {
      var result = new BuildConfig();
      if (!file.Exists)
        _logger.LogWarning("Settings file {file} not found!", file.Name);
      else
      {
        _logger.LogInformation("Loading settings from {file}...", file.Name);
        result = From(ConfigSource.From(file));
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
