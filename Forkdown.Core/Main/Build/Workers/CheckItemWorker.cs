using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Types;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Build.Workers {
  /// <summary>
  /// Convert appropriate list items into check items.
  /// </summary>
  public class CheckItemWorker : Worker, IDocumentWorker { 
    // Make it a full-document worker so that it's looking further down the tree and modifying elements doesn't mess
    // with the processing order.

    public override Element ProcessElement(Element element, Arguments args) {
      if (element is BlockContainer && element.Subs.Any(_ => _ is Listing l && l.Kind == ListingKind.Checklist)) {
        var newSubs = Nil.L<Element>();
        element.Subs.ForEach(_ => {
          if (_ is Listing list && list.Kind == ListingKind.Checklist) {
            list.Subs.ForEach(item => {
              newSubs.Add(new CheckItem(item) { IsNewline = list.IsVertical });
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
