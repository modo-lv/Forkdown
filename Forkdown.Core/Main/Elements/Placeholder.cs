using System;
using Markdig.Syntax;

namespace Forkdown.Core.Main.Elements {
  public class Placeholder : Inline {
    public String Name;

    public Placeholder(MarkdownObject node) : base(node) {
      this.Name = node.GetType().Name;
    }
  }
}