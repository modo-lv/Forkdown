using System;
using Markdig.Syntax;
using Block = Forkdown.Core.Elements.Types.Block;

namespace Forkdown.Core.Elements {
  public enum ParagraphKind {
    Normal, Info, Warning, Help
  }
  
  public class Paragraph : Element, Block {
    public ParagraphKind Kind = ParagraphKind.Normal;

    /// <summary>
    /// Is this paragraph part of a title, such as from a single newline in a checkbox?
    /// </summary>
    public Boolean IsTitle;
    
    public Paragraph() { }
    public Paragraph(IMarkdownObject mdo) : base(mdo) { }
  }
}