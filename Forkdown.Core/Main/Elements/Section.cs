using System;
using Markdig.Syntax;
using Block = Forkdown.Core.Elements.Types.Block;

namespace Forkdown.Core.Elements {
  public class Section : Element, Block {

    public String CheckboxId = "";
    
    /// <summary>
    /// Was this section created implicitly from a heading, as opposed to being explicitly defined in markdown?
    /// </summary>
    public Boolean IsImplicit = false;
    
    public Section() {} 

    protected Section(MarkdownObject mdo) : base(mdo) {
      
    }
  }
}
