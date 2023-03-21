using Forkdown.Core.Elements.Types;
using Markdig.Syntax;

namespace Forkdown.Core.Elements; 

public class ListItem : BlockContainer {
  public ListItem() { }
  public ListItem(IMarkdownObject mdo) : base(mdo) { }
}