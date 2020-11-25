using System;
using Forkdown.Core.Config.Input;
using YamlDotNet.Serialization.NamingConventions;

namespace Forkdown.Core.Config {
  public partial class MainConfig : MainConfigInput {
    public new ExternalLinkConfig ExternalLinks { get; set; } = new ExternalLinkConfig();

    public MainConfig() { }

    public MainConfig(MainConfigInput input) {
      this.Name = input.Name;
      this.Labels = input.Labels;
      this.Tips = input.Tips;
      this.ExternalLinks = input.ExternalLinks.ToConfig();
    }
    public static readonly YamlDotNet.Serialization.Deserializer Yaml = new YamlDotNet.Serialization.DeserializerBuilder()
      .WithNamingConvention(new UnderscoredNamingConvention())
      .IgnoreUnmatchedProperties()
      .Build();
  }
}
