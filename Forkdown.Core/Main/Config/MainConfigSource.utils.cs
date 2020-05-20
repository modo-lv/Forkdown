using System;
using System.Collections.Generic;
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
  }
}
