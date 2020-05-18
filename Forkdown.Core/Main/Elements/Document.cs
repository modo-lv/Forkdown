using System;
using Forkdown.Core.Main.Elements;
using Markdig.Syntax;

namespace Forkdown.Core.Elements {
  public class Document : Container {
    public String FileName = "";

    public Document(MarkdownDocument document) : base(document) { }
  }
}