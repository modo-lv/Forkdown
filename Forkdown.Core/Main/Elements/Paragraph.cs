using Markdig.Syntax;
using Block = Forkdown.Core.Elements.Types.Block;

namespace Forkdown.Core.Elements {
  public enum ParagraphKind {
    Normal, Info, Warning, X
  }
  
  public class Paragraph : Element, Block {
    public ParagraphKind Kind = ParagraphKind.Normal;
    
    public Paragraph() { }
    public Paragraph(IMarkdownObject mdo) : base(mdo) { }
  }
}