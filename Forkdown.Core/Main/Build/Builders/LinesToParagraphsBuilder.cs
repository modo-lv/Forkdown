using System;
using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Types;
using Simpler.NetCore;
using static MoreLinq.Extensions.GroupAdjacentExtension;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Build.Builders {
  public class LinesToParagraphsBuilder : Builder {
    public override TElement BuildTree<TElement>(TElement root, Maybe<Object> objects) {
      if (!(root is BlockContainer))
        return root;
      
      var newSubs = Nil.L<Element>();
      foreach (Element el in root.Subs) {
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
      root.Subs = newSubs;

      return root;
    }
  }
}
