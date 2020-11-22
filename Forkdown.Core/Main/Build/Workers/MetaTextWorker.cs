using Forkdown.Core.Elements;

namespace Forkdown.Core.Build.Workers {
  public class MetaTextWorker : Worker {
    public override TElement BuildElement<TElement>(TElement element) {
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
