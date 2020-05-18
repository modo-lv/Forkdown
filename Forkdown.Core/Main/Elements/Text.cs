using System;
using Markdig.Syntax.Inlines;
using Inline = Forkdown.Core.Main.Elements.Inline;

namespace Forkdown.Core.Elements {
  public class Text : Inline {
    public String Content;

    public Text(LiteralInline text) : base(text) =>
      this.Content = text.Content.ToString();
  }
}
