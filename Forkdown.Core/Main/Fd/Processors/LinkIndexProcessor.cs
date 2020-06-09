using Forkdown.Core.Elements;
using Forkdown.Core.Fd.Contexts;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Fd.Processors {
  public class LinkIndexProcessor : IElementProcessor {

    public T ProcessElement<T>(T element, Arguments args, IContext context) where T : Element {
      element.GlobalIds.ForEach(_ =>
        context.Proj<InternalLinks>().Add(_, context.Document)
      );
      return element;
    }
  }
}
