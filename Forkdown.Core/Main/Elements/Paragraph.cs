using Markdig.Syntax;
using Block = Forkdown.Core.Elements.Types.Block;

namespace Forkdown.Core.Elements {
  public class Paragraph : Element, Block {
    public Paragraph() { }
    public Paragraph(IMarkdownObject mdo) : base(mdo) { }
  }
}