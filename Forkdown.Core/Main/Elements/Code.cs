using System;
using Forkdown.Core.Internal;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace Forkdown.Core.Main.Elements {
  public class Code : Inline {
    public readonly String Content;
    
    public Code(MarkdownObject node) : base(node) {
      this.Content = node.As<CodeInline>().Content;
    }
  }
}