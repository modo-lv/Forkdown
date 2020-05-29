using System;
using Forkdown.Core.Elements.Types;
using Markdig.Syntax;

namespace Forkdown.Core.Elements {
  public class ListItem : BlockContainer {
    public Boolean IsCheckbox;
    public Char BulletType;
  }
}
