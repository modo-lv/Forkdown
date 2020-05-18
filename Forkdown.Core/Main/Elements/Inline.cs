using Forkdown.Core.Elements;
using Markdig.Syntax;

namespace Forkdown.Core.Main.Elements {
  public abstract class Inline : Element {
    protected Inline(MarkdownObject mdo) : base(mdo) { }
  }
}