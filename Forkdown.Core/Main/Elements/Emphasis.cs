using System;
using Forkdown.Core.Internal;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace Forkdown.Core.Main.Elements {
  public class Emphasis : Inline {
    public Boolean IsStrong = false;

    public Emphasis(MarkdownObject node) : base(node) {
      this.IsStrong = node.As<EmphasisInline>().DelimiterCount == 2;
    }
  }
}