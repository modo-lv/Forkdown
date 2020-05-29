using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  public interface IForkdownProcessor {
    T ProcessElement<T>(T element, IDictionary<String, Object> context) where T : Element;
    
    T ProcessElementAndSubs<T>(T element, IDictionary<String, Object>? context = null) where T : Element {
      context ??= Nil.DStr<Object>();
      element = this.ProcessElement(element, context!);
      element.Subs = element.Subs.Select(_ => ProcessElementAndSubs(_, context)).ToList();
      return element;
    }
  }
}
