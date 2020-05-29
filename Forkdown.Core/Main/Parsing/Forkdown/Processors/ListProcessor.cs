using System;
using System.Collections.Generic;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  public class ListProcessor : IForkdownProcessor{

    public void Process<T>(T element) where T : Element {
      element.Subs.ForEach(el => {
        if (element is Listing list && el is ListItem li) {
          li.BulletType = list.BulletType;
        }
        this.Process(el);
      });
    }
  }
}
