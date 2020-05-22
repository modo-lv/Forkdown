using System;
using Markdig.Syntax.Inlines;

// ReSharper disable NotAccessedField.Global

namespace Forkdown.Core.Elements {
  public class Emphasis : Element, Types.Inline {
    public Boolean IsStrong = false;

    public Emphasis(EmphasisInline emphasis) : base(emphasis) =>
      this.IsStrong = emphasis.DelimiterCount > 1;
  }
}
