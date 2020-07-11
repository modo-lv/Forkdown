using System;
using System.Linq;
using Forkdown.Core.Elements;
using static MoreLinq.Extensions.InsertExtension;
using static MoreLinq.Extensions.GroupAdjacentExtension;

namespace Forkdown.Core.Build.Workers {
  /// <summary>
  /// Split the lines of a checkbox title into paragraphs.
  /// </summary>
  public class CheckitemTitleSplitWorker : Worker, IDocumentWorker {
    public override Element ProcessElement(Element element, Arguments args) {
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
      return element;
    }
  }
}
