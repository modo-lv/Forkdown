using System;

namespace Forkdown.Core.Elements {
  public class Label : Element {
    public readonly String Key;
    public String Name;
    public String Icon = "";

    public Label(String key) {
      Key = key.Trim();
      Name = this.Key;
    }
  }
}
