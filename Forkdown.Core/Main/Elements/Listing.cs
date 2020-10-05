using System;
using Markdig.Syntax;
using Block = Forkdown.Core.Elements.Types.Block;

// ReSharper disable NotAccessedField.Global

namespace Forkdown.Core.Elements {
  public enum ListingKind {
    Unordered,
    Ordered,
    Checklist,
  }

  public class Listing : Element, Block {
    public readonly ListingKind Kind = ListingKind.Unordered;

    public Boolean IsVertical;
    public Boolean IsHorizontal { get => !IsVertical; set => IsVertical = !value; }

    public Listing() { }

    public Listing(ListBlock mdo) : base(mdo) {
      if (mdo.IsOrdered)
        this.Kind = ListingKind.Ordered;
      else switch (mdo.BulletType) {
        case '+':
          this.Kind = ListingKind.Checklist;
          this.IsVertical = true;
          break;
        case '-':
          this.Kind = ListingKind.Checklist;
          this.IsVertical = false;
          break;
      }
    }
  }
}
