using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Attributes;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Build.Workers {
  /// <summary>
  /// Process help, info and warning paragraphs.
  /// </summary>
  public class MetaTextWorker : Worker {
    public override Element ProcessElement(Element element, Arguments args) {
      element.Kind = element.Settings switch {
        {} s when s.IsTrue("!") || s.IsTrue("warn") => Element.Metadata.Warning,
        {} s when s.IsTrue("i") => Element.Metadata.Info,
        {} s when s.IsTrue("?") || s.IsTrue("help") => Element.Metadata.Help,
        _ => element.Kind
      };
      return element;
    }
  }
}
