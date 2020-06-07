using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  /// <summary>
  /// A processor that handles an entire document at once.
  /// </summary>
  public interface IDocumentProcessor : IElementProcessor {
    Document Process(Document doc) {
      return proc(doc, Nil.DStr<Object>());

      T proc<T>(T el, IDictionary<String, Object> args) where T : Element{
        el = this.Process(el, args);
        el.Subs = el.Subs
          .Select(e => proc(e, new Dictionary<String, Object>(args)))
          .ToList();
        return el;
      }
    }
  }
}
