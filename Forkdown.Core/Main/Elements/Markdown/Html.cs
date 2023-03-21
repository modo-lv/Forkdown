using System;
using Markdig.Syntax;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Elements.Markdown; 

public class Html : Element {
  // ReSharper disable once NotAccessedField.Global
  public String Content;
    
  public Html(LeafBlock mdo) : base(mdo) {
    this.Content = mdo.Lines.StringJoin();
  }
}