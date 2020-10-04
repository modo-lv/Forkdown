using System;
using System.Linq;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;
using static MoreLinq.Extensions.InsertExtension;
using static MoreLinq.Extensions.GroupAdjacentExtension;

namespace Forkdown.Core.Build.Workers {
  /// <summary>
  /// Split lines into paragraphs.
  /// </summary>
  public class LineBreakToParagraphWorker : Worker, IDocumentWorker {
    public override Element ProcessElement(Element element, Arguments args) {
      /*
      element.Subs = element.Subs.SelectMany(el => {
        if (el is Paragraph && el.Subs.Any(_ => _ is LineBreak)) {
          for (var a = 0; a < el.Subs.Count - 1; a++) {
            if (el.Subs[a] is LineBreak lb && el.Subs[a+1] is Text t && t.Content.StartsWith("  "))
          }
          var grouped = el.Subs.GroupAdjacent(_ => _ is LineBreak);
          grouped.Where(_ => _.Key == false)
        }
        else {
          return new[] { el };
        }
      }).ToList();
      */
      /*
      if (element is ListItem li && li.Subs.FirstOrDefault() is Paragraph p) {
        if (p.Subs.Any(_ => _ is LineBreak)) {
          var pGroups = p.Subs.GroupAdjacent(_ => !(_ is LineBreak));
          var tParas = pGroups
            .Where(_ => _.Key)
            .Select(elements => (Element) new Paragraph {
              IsTitle = true,
              Subs = elements.ToList(),
            });

          li.Subs.RemoveAt(0);
          li.Subs = li.Subs.Insert(tParas, 0).ToList();
          li.Subs.First().Attributes = p.Attributes;
        }
        else {
          p.IsTitle = true;
        }
      }
      */
      return element;
    }
  }
}
