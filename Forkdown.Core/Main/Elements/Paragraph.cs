using Forkdown.Core.Main.Elements;
using Markdig.Syntax;

namespace Forkdown.Core.Elements {
  public class Paragraph : Container {
    public Paragraph(ParagraphBlock paragraph) : base(paragraph) { }
  }
}