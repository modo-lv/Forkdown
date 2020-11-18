using System;

namespace Forkdown.Core.Config {
  public partial class LabelConfig {
    public String Key;

    public String Name;

    public String Icon;

    public LabelConfig(String key, String name, String icon) {
      Key = key;
      Name = name;
      Icon = icon;
    }
  }
}
