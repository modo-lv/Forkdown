using Markdig.Syntax;
using Block = Forkdown.Core.Elements.Types.Block;

namespace Forkdown.Core.Elements {
  public class ListItem : Element, Block {

    public ListItem(ListItemBlock mdo) : base(mdo) { }
  }
}
