using Markdig.Syntax;

namespace Forkdown.Core.Main.Elements {
  public abstract class Container : Element {
    protected Container(MarkdownObject node) : base(node) { }
  }
}