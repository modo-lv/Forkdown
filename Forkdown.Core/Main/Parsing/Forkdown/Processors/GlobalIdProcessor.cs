using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  public class GlobalIdProcessor : IForkdownProcessor {

    public T Process<T>(T element, IDictionary<String, Object> args) where T : Element {
      element.GlobalIds = element
        .Settings.Keys.ToList()
        .Where(_ => _.Text().StartsWith("#"))
        .Select(_ => {
          element.Settings.Remove(_);
          return GlobalId.From(_.Part(1).Replace("~", element.Title));
        })
        .ToList();
      return element;
    }
  }
}
