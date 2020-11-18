using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Types;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Build.Workers {
  /// <summary>
  /// Convert appropriate list items into check items.
  /// </summary>
  public class ItemWorker : Worker, IDocumentWorker {
    // Full-document worker so that its looking further down the tree and modifying elements doesn't mess
    // with the processing order.

    public override Element ProcessElement(Element element, Arguments args) {
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
