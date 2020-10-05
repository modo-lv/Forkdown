using System;
using Forkdown.Core.Elements.Types;

namespace Forkdown.Core.Elements {
  public class ListItem : BlockContainer {

    public override Boolean IsCheckItem => this.FindSub<Article>()?.IsCheckItem ?? false;
  }
}
