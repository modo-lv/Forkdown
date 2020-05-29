using Markdig.Syntax;
using Block = Forkdown.Core.Elements.Types.Block;

namespace Forkdown.Core.Elements {
  public class ExplicitContainer : Element, Block {
    public ExplicitContainer() { }
    public ExplicitContainer(IMarkdownObject mdo) : base(mdo) { }
  }
}
