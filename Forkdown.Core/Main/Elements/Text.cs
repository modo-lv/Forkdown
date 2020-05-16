using System;
using Forkdown.Core.Internal;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace Forkdown.Core.Main.Elements {
  public class Text : Inline {
    public String Content;
    
    public Text(MarkdownObject node) : base(node) {
      this.Content = node.As<LiteralInline>().Content.ToString();
    }
  }
}