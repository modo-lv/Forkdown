using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  public class ListProcessor : IForkdownProcessor{

    public T Process<T>(T element, Document? doc = null) where T : Element {
      if (element is Listing list) {
        list.Subs.ForEach(_ => (_ as ListItem)!.BulletType = list.BulletType);
      }
      element.Subs.ForEach(_ => Process(_, doc));
      return element;
    }
  }
}
