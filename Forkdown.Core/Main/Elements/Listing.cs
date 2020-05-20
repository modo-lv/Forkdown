using System;
using Markdig.Syntax;

// ReSharper disable NotAccessedField.Global

namespace Forkdown.Core.Elements {
  public class Listing : Container {

    public Boolean IsOrdered;

    public Listing(ListBlock mdo) : base(mdo) =>
      this.IsOrdered = mdo.IsOrdered;
  }
}
