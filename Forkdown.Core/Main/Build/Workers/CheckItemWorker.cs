using System;
using Forkdown.Core.Elements;

namespace Forkdown.Core.Build.Workers {
  /// <summary>
  /// Convert appropriate list items into check items.
  /// </summary>
  public class CheckItemWorker : Worker {

    public override Element ProcessElement(Element element, Arguments args) {
      var inList = args.Get<Boolean>();

      if (inList) {
        if (element is ListItem li) {
          element = new CheckItem(li);
        }
        else {
          inList = false;
        }
      }
      if (element is Listing list && list.Kind == ListingKind.Checklist) {
        inList = true;
      }

      args.Put(inList);
      
      return element;
    }
  }
}
