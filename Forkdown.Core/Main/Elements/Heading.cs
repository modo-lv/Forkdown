using System;
using Forkdown.Core.Internal;
using Markdig.Syntax;

namespace Forkdown.Core.Main.Elements {
  public class Heading : Container {
    public Int32 Level;

    public Heading(MarkdownObject node) : base(node) {
      this.Level = node.As<HeadingBlock>().Level;
    }
  }
}