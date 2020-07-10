using System;
using Forkdown.Core.Elements.Types;

namespace Forkdown.Core.Elements {
  public class ListItem : BlockContainer {
    public Char BulletChar;

    public override Boolean IsCheckItem => this.FindSub<Article>()?.IsCheckItem ?? false;
    public String CheckItemId => this.FindSub<Article>()?.GlobalId ?? "";
  }
}
