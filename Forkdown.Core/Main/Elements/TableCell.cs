using Markdig.Syntax;
using Block = Forkdown.Core.Elements.Types.Block;

namespace Forkdown.Core.Elements; 

public class TableCell : Element, Block {
  public TableCell(IMarkdownObject mdo) : base(mdo) { }
}