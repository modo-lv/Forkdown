using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  public class ListItemProcessor : IElementProcessor {

    public T Process<T>(T element, IDictionary<String, Object> args) where T : Element {
      if (element is ListItem li &&
          li.Subs.FirstOrDefault() is Paragraph par) {
        par.MoveAttributesTo(li);
      }
      return element;
    }
  }
}
