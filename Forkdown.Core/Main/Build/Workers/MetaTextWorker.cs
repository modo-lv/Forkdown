using System.Linq;
using Forkdown.Core.Elements;

namespace Forkdown.Core.Build.Workers {
  public class MetaTextWorker : Worker {
    public override Element BuildElement(Element element) {
      if (element is Paragraph p && p.Subs.FirstOrDefault() is Code c && c.Content.StartsWith(":")) {
        p.Kind = c.Content.Substring(1) switch {
          {} s when s == "!" || s == "warn" => Element.Metadata.Warning,
          {} s when s == "i" => Element.Metadata.Info,
          {} s when s == "?" || s == "help" => Element.Metadata.Help,
          _ => p.Kind
        };
        p.Subs.RemoveAt(0);
        if (p.Subs.FirstOrDefault() is Text t)
          t.Content = t.Content.TrimStart();
      }
      return element;
    }
  }
}
