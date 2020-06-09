using Forkdown.Core.Elements;
using Forkdown.Core.Fd.Contexts;

namespace Forkdown.Core.Fd.Processors {
  public class LinkProcessor : IElementProcessor {

    public virtual T ProcessElement<T>(T element, Arguments args, IContext context) where T : Element {
      if (element is Link link && link.Target == "@") {
        link.Target = $"@{link.Title}";
      }
      
      return element;
    }
  }
}
