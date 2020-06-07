using System;
using System.Collections.Generic;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  public class LinkProcessor : IElementProcessor {

    public T Process<T>(T element, IDictionary<String, Object> args) where T : Element {
      if (element is Link link && link.Target == "@") {
        link.Target = $"@{link.Title}";
      }
      return element;
    }
  }
}
