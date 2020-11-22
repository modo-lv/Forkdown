using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Types;
using Simpler.NetCore.Collections;
using static MoreLinq.Extensions.GroupAdjacentExtension;

namespace Forkdown.Core.Build.Workers {
  public class LinesToParagraphsWorker : Worker {
    public override TElement BuildElement<TElement>(TElement element) {
      if (!(element is BlockContainer))
        return element;
      
      var newSubs = Nil.L<Element>();
      foreach (Element el in element.Subs) {
        if (el is Paragraph p && p.Subs.Any(_ => _ is LineBreak)) {
          var lineSubs = Nil.L<Element>();
          p.Subs.GroupAdjacent(_ => _ is LineBreak).Where(_ => !_.Key).ForEach(_ => {
            lineSubs.Add(new Paragraph {
              Subs = _.ToList()
            });
          });
          if (lineSubs.Any())
            p.MoveAttributesTo(lineSubs.First());
          newSubs = newSubs.Concat(lineSubs).ToList();
        }
        else {
          newSubs.Add(el);
        }
      }
      
      element.Subs = newSubs;

      return element;
    }
  }
}
