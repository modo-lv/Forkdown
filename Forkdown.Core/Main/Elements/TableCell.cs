using Markdig.Syntax;
using Block = Forkdown.Core.Elements.Types.Block;
using Tables = Markdig.Extensions.Tables;

namespace Forkdown.Core.Elements {
  public class TableCell : Element, Block {
    public TableCell(IMarkdownObject mdo) : base(mdo) { }
  }
}
