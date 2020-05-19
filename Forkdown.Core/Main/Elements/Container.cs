using Markdig.Syntax;

namespace Forkdown.Core.Elements {
  public abstract class Container : Element {
    protected Container(MarkdownObject mdo) : base(mdo) {}
  }
}