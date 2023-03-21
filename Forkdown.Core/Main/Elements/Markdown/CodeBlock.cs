using System;
using Markdig.Syntax;
using Block = Forkdown.Core.Elements.Types.Block;

// ReSharper disable NotAccessedField.Global

namespace Forkdown.Core.Elements.Markdown; 

public class CodeBlock : Element, Block {
  public readonly String Content = "";

  public CodeBlock(IMarkdownObject mdo) : base(mdo) {
    if (mdo is FencedCodeBlock cb) {
      this.Content = cb.Lines.ToString();
    }
  }
}