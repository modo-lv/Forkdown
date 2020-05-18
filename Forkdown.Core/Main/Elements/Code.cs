using System;
using Markdig.Syntax.Inlines;
using Inline = Forkdown.Core.Main.Elements.Inline;

// ReSharper disable NotAccessedField.Global

namespace Forkdown.Core.Elements {
  public class Code : Inline {
    public readonly String Content;

    public Code(CodeInline code) : base(code) =>
      this.Content = code.Content;
  }
}
