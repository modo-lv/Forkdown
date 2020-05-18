using System;
using Markdig.Syntax.Inlines;
using Inline = Forkdown.Core.Main.Elements.Inline;

// ReSharper disable NotAccessedField.Global

namespace Forkdown.Core.Elements {
  public class Emphasis : Inline {
    public Boolean IsStrong = false;

    public Emphasis(EmphasisInline emphasis) : base(emphasis) =>
      this.IsStrong = emphasis.DelimiterCount > 1;
  }
}
