using System;
using System.IO;
using Forkdown.Core.Wiring;
using Microsoft.Extensions.Logging;
using Simpler.NetCore.Text;
using Yaml = YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Path = Fluent.IO.Path;

namespace Forkdown.Core.Config {
  public partial class MainConfig {
    private static readonly ILogger<MainConfig> _logger = Program.Logger<MainConfig>();


    /// <summary>
    /// Load a project's main settings file.
    /// </summary>
    /// <param name="file">File to read settings from.</param>
    /// <returns>Default project settings selectively overridden by values from the file.</returns>
    public static MainConfig FromYaml(FileInfo file) {
      MainConfig result;
      if (!file.Exists)
      {
        _logger.LogWarning("Settings file {file} not found!", file.Name);
        result = new MainConfig();
      }
      else
      {
        _logger.LogInformation("Loading settings from {file}...", file.Name);

        using var stream = file.OpenRead();
        using var reader = new StreamReader(stream);

        Yaml.Deserializer parser = new Yaml.DeserializerBuilder()
          .WithNamingConvention(new UnderscoredNamingConvention())
          .IgnoreUnmatchedProperties()
          .Build();

        result = parser.Deserialize<MainConfig>(reader);
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
