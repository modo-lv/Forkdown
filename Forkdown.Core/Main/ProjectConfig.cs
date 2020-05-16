using System;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Forkdown.Core.Main {
  // ReSharper disable once ClassNeverInstantiated.Global
  public class ProjectConfig {
    private String _name = "";

    public String Name
    {
      get => this._name;
      set => this._name = value ?? "";
    }


    private static ILogger<ProjectConfig> _Logger = Program.Logger<ProjectConfig>();

    public static ProjectConfig FromYaml(FileInfo file) {
      if (!file.Exists)
      {
        _Logger.LogInformation("Project settings file {file} not found, using defaults.", file.Name);
        return new ProjectConfig();
      }
      
      _Logger.LogInformation("Loading project settings from {file}...", file.Name);
      
      using var stream = file.OpenRead();
      using var reader = new StreamReader(stream);

      var parser = new DeserializerBuilder()
        .WithNamingConvention(new UnderscoredNamingConvention())
        .IgnoreUnmatchedProperties()
        .Build();

      return parser.Deserialize<ProjectConfig>(reader);
    }
  }
}