using Forkdown.Core.Elements;
using Markdig.Syntax;

namespace Forkdown.Core.Main.Elements {
  public abstract class Container : Element {
    protected Container(MarkdownObject mdo) : base(mdo) { }
  }
}