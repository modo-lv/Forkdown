using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Markdown;

namespace Forkdown.Core.Build.Workers; 

public class MetaTextWorker : Worker {
  public override Element BuildElement(Element element) {
    switch (element) {
      case Paragraph p when p.Subs.FirstOrDefault() is Code c && c.Content.StartsWith(":"): {
        p.Kind = c.Content.Substring(1) switch {
          {} w when w == "!" || w == "warn" => Element.Metadata.Warning,
          {} i when i == "i" || i == "info" => Element.Metadata.Info,
          {} h when h == "?" || h == "help" => Element.Metadata.Help,
          _ => p.Kind
        };
        p.Subs.RemoveAt(0);
        if (p.Subs.FirstOrDefault() is Text t)
          t.Content = t.Content.TrimStart();
        break;
      }
      case Section s:
        s.Kind = s switch {
          {} w when w.Settings.IsTrue("warn") => Element.Metadata.Warning,
          {} i when i.Settings.IsTrue("info") => Element.Metadata.Info,
          {} h when h.Settings.IsTrue("help") => Element.Metadata.Help,
          _ => element.Kind
        };
        break;
    }
    return element;
  }
}