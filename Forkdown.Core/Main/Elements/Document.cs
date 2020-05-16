using System;
using Markdig.Syntax;

namespace Forkdown.Core.Main.Elements {
  public class Document : Container {
    public String FileName = "";

    public Document(MarkdownObject node) : base(node) { }
  }
}