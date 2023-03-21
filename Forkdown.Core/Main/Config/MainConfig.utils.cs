using System;
using System.Linq;
using System.Text;
using Fluent.IO;
using Forkdown.Core.Config.Input;
using Simpler.NetCore.Collections;
using YamlDotNet.Serialization.NamingConventions;

namespace Forkdown.Core.Config; 

public partial class MainConfig {
  /// <summary>
  /// Configuration file YAML deserializer.
  /// </summary>
  public static readonly YamlDotNet.Serialization.Deserializer Yaml =
    new YamlDotNet.Serialization.DeserializerBuilder()
      .WithNamingConvention(new UnderscoredNamingConvention())
      .IgnoreUnmatchedProperties()
      .Build();

    
  /// <summary>
  /// Parse all configuration files in a directory into a single configuration object.
  /// </summary>
  /// <param name="directory">Directory where the configuration files are located.</param>
  /// <param name="pattern">Config file pattern.</param>
  /// <returns></returns>
  public static MainConfig FromFilesIn(Path directory, String pattern = "*.config") => FromYaml(
    directory.Files(pattern, false).Select(f => f.Read(Encoding.UTF8)).StringJoin(Environment.NewLine)
  );

    
  /// <summary>
  /// Parse YAML text into a configuration object.  
  /// </summary>
  public static MainConfig FromYaml(String yaml) => Yaml.Deserialize<MainConfigInput>(yaml).ToConfig();
}