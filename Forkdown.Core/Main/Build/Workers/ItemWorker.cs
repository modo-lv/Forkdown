using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Types;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Build.Workers {
  public class ItemWorker : Worker {
    public ItemWorker() {
      this.RunsAfter<ListItemWorker>();
      this.RunsAfter<LinesToParagraphsWorker>();
    }

    public override TElement BuildElement<TElement>(TElement element) {
      if (element is BlockContainer &&
          element.Subs.Any(_ => _ is Listing l && l.Kind.IsOneOf(ListingKind.Items, ListingKind.CheckItems))) 
      {
        var newSubs = Nil.L<Element>();
        element.Subs.ForEach(_ => {
          if (_ is Listing list && list.Kind.IsOneOf(ListingKind.Items, ListingKind.CheckItems)) {
            list.Subs.ForEach(item => {
              newSubs.Add(new Item(item, list.Kind));
            });
          }
          else {
            newSubs.Add(_);
          }
        });
        element.Subs = newSubs;
      }

      return element;
    }
  }
}
