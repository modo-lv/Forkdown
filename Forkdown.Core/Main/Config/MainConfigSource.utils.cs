using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization.NamingConventions;
using Yaml = YamlDotNet.Serialization;

namespace Forkdown.Core.Config {
  public partial class MainConfigSource {
    private static readonly Yaml.Deserializer _parser = new Yaml.DeserializerBuilder()
      .WithNamingConvention(new UnderscoredNamingConvention())
      .IgnoreUnmatchedProperties()
      .Build();

    public static MainConfigSource From(String yaml) => 
      new MainConfigSource(_parser.Deserialize<IDictionary<Object, Object>>(yaml));
    
    public static MainConfigSource From(FileInfo file) {
      using var stream = file.OpenRead();
      using var reader = new StreamReader(stream);

      return new MainConfigSource(_parser.Deserialize<IDictionary<Object, Object>>(reader));
    }
    
  }
}
