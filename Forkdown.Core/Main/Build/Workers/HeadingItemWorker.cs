using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Build.Workers {
  public class HeadingItemWorker : Worker {
    public HeadingItemWorker() {
      this.RunsAfter<LinesToParagraphsWorker>();
    }

    public override Element BuildElement(Element element) {
      { // Headings can't contain other headings & list item headings don't become items.
        if (element is Heading || element is ListItem)
          return element;
      }

      Item? item = null;
      Heading? lastHeading = null;
      var subs = element switch {
        Item i => i.Content,
        _ => element.Subs
      };
      var newSubs = Nil.L<Element>();
      if (element is Item ii)
        newSubs.Add(ii.Title);

      subs.ForEach(el => {
        var newItem = el is Heading h && h.Level <= (lastHeading?.Level ?? 7);
        if (item == null) {
          if (newItem)
            item = Item.FromTitleElement(el);
          else
            newSubs.Add(el);
        }
        else {
          if (newItem) {
            newSubs.Add(item);
            item = Item.FromTitleElement(el);
          }
          else {
            item.Subs.Add(el);
          }
        }

        if (newItem)
          lastHeading = el as Heading;

      });
      if (item != null) {
        newSubs.Add(item);
      }

      element.Subs = newSubs;

      return element;
    }
  }
}
