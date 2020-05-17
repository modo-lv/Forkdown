using System;
using System.IO;
using Forkdown.Core.Wiring;
using Microsoft.Extensions.Logging;
using Simpler.NetCore.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Path = Fluent.IO.Path;

namespace Forkdown.Core.Main.ProjectConfig {
  // ReSharper disable once ClassNeverInstantiated.Global
  public partial class ProjectConfig {
    public const String FileName = "forkdown.core.yaml";

    private String _name = "";
    public String Name
    {
      get => this._name;
      set => this._name = value ?? "";
    }

    public String Prefix = "FD_";
    
    public ExternalLinks ExternalLinks { get; set; } = new ExternalLinks();
    

    
    private static ILogger<ProjectConfig> _Logger = Program.Logger<ProjectConfig>();
    
    public static ProjectConfig FromYaml(Path projectRoot) {
      var file = projectRoot.File(ProjectConfig.FileName);
      ProjectConfig result;
      if (!file.Exists)
      {
        _Logger.LogWarning("Settings file {file} not found!", file.Name);
        result = new ProjectConfig();
      }
      else
      {
        _Logger.LogInformation("Loading settings from {file}...", file.Name);

        using var stream = file.OpenRead();
        using var reader = new StreamReader(stream);

        var parser = new DeserializerBuilder()
          .WithNamingConvention(new UnderscoredNamingConvention())
          .IgnoreUnmatchedProperties()
          .Build();

        result = parser.Deserialize<ProjectConfig>(reader);
      }

      if (result.Name.IsBlank())
      {
        result.Name = projectRoot.FileName;
        _Logger.LogWarning("Project name not found in settings, using unreliable folder name \"{name}\"!", result.Name);
      }

      return result;
    }
  }
}