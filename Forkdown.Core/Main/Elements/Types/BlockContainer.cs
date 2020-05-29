using System;

namespace Forkdown.Core.Elements.Types {
  /// <summary>
  /// An element that can contain other block elements.
  /// </summary>
  public abstract class BlockContainer : Element, Block {
    /// <summary>
    /// This element's individual checkbox ID, used in generating a full checkbox ID for
    /// this element (if checkbox) and/or checkboxes it contains. 
    /// </summary>
    public String CheckboxId = "";
  }
}
