using Markdig.Syntax;
using Block = Forkdown.Core.Elements.Types.Block;

namespace Forkdown.Core.Elements {
  public class Table : Element, Block {
    public Table(IMarkdownObject mdo) : base(mdo) { }
  }
}
