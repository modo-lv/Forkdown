using System;
using System.Collections.Generic;
using Forkdown.Core.Elements;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  public interface IForkdownProcessor {
    T Process<T>(T element, IDictionary<String, Object> args) where T : Element;
  }
}
