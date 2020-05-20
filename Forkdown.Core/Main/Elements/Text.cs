using System;
using Markdig.Syntax.Inlines;
using Inline = Forkdown.Core.Elements.Types.Inline;

namespace Forkdown.Core.Elements {
  public class Text : Element, Types.Inline {
    public readonly String Content;

    public Text(LiteralInline text) : base(text) =>
      this.Content = text.Content.ToString();
  }
}
