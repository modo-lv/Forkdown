using System;
using Markdig.Syntax;
using Block = Forkdown.Core.Elements.Types.Block;

// ReSharper disable NotAccessedField.Global

namespace Forkdown.Core.Elements {
  public class Listing : Element, Block {

    public Boolean IsOrdered;

    public Listing(ListBlock mdo) : base(mdo) =>
      this.IsOrdered = mdo.IsOrdered;
  }
}
