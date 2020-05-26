using System;
using Block = Forkdown.Core.Elements.Types.Block;

// ReSharper disable NotAccessedField.Global

namespace Forkdown.Core.Elements {
  /// <summary>
  /// An element that functions as a section, often with a distinct title/heading.
  /// </summary>
  public class Section : Element, Block {
    /// <summary>
    /// This element's ID, used in generating a full checkbox ID on this element (if checkbox) or checkboxes
    /// it contains. 
    /// </summary>
    public String CheckboxId = "";
    
    /// <summary>
    /// Was this section created implicitly from a heading, as opposed to being explicitly defined in markdown?
    /// </summary>
    public Boolean IsImplicit = false;

    /// <summary>
    /// Level of the heading that the section is identified by (or 0 if N/A).  
    /// </summary>
    public Int32 HeadingLevel = 0;

    

    protected Section() { }
    public Section(Heading h) => this.MergeWith(h);

    

    public Section MergeWith(Heading h) {
      this.HeadingLevel = h.Level;
      this.Attributes = h.Attributes;
      this.IsImplicit = true;
      h.Attributes = new ElementAttributes();
      return this;
    }
  }
}
