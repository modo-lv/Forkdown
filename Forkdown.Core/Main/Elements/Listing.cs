using Markdig.Syntax;
using Block = Forkdown.Core.Elements.Types.Block;

// ReSharper disable NotAccessedField.Global

namespace Forkdown.Core.Elements; 

public enum ListingKind {
  Unordered,
  Ordered,
  Items,
  CheckItems,
}

public class Listing : Element, Block {
  public new readonly ListingKind Kind = ListingKind.Unordered;

  public Listing(ListBlock mdo) : base(mdo) {
    if (mdo.IsOrdered)
      this.Kind = ListingKind.Ordered;
    else
      this.Kind = mdo.BulletType switch {
        '+' => ListingKind.CheckItems,
        '-' => ListingKind.Items,
        _ => this.Kind
      };
  }
}