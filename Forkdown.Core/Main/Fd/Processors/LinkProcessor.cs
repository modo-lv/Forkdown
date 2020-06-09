using Forkdown.Core.Elements;
using Forkdown.Core.Fd.Contexts;

namespace Forkdown.Core.Fd.Processors {
  public class LinkProcessor : IElementProcessor {

    public virtual Result<T> Process<T>(T element, IContext context) where T : Element {
      if (element is Link link && link.Target == "@") {
        link.Target = $"@{link.Title}";
      }
      
      return context.ToResult(element);
    }
  }
}
