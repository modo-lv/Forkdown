using System;

namespace Forkdown.Core.Config {
  public class LabelConfig {
    public String Key { get; set; } = "";

    public String Name { get; set; } = "";

    public String Icon { get; set; } = "";

    public LabelConfig() { }

    public LabelConfig(String key, String name, String icon) {
      Key = key;
      Name = name;
      Icon = icon;
    }
  }
}
