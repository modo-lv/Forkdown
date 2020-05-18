using System;
using Forkdown.Core.Main.Elements;
using Markdig.Syntax;

// ReSharper disable NotAccessedField.Global

namespace Forkdown.Core.Elements {
  public class Placeholder : Inline {
    public String Name;

    public Placeholder(MarkdownObject mdo) : base(mdo) => 
      this.Name = mdo.GetType().Name;
  }
}
