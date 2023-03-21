using System;
using Markdig.Syntax.Inlines;
using Inline = Forkdown.Core.Elements.Types.Inline;

// ReSharper disable NotAccessedField.Global

namespace Forkdown.Core.Elements.Markdown; 

public class Code : Element, Inline {
  public readonly String Content;

  public Code(CodeInline code) : base(code) =>
    this.Content = code.Content;
}