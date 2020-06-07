using System;
using Forkdown.Core.Elements.Types;
using Markdig.Syntax;

// ReSharper disable NotAccessedField.Global

namespace Forkdown.Core.Elements {
  public class Placeholder : Element, Inline {
    public String Name;

    public Placeholder(IMarkdownObject mdo) : base(mdo) => 
      this.Name = mdo.GetType().Name;
  }
}
