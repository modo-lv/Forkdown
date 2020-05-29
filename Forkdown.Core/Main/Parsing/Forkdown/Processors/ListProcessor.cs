using System;
using System.Collections.Generic;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  public class ListProcessor : IForkdownProcessor{

    public T ProcessElement<T>(T element, IDictionary<String, Object> context) where T : Element {
      if (element is Listing list) {
        list.Subs.ForEach(_ => (_ as ListItem)!.BulletType = list.BulletType);
      }
      return element;
    }
  }
}
