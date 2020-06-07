using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  public class ListItemProcessor : IElementProcessor {

    public T Process<T>(T element, IDictionary<String, Object> args) where T : Element {
      switch (element) {
        case Listing list:
          args["bulletChar"] = list.BulletChar;
          break;
        case ListItem li: {
          li.BulletChar = (Char) args.GetOr("bulletChar", '*');
          if (li.Subs.FirstOrDefault() is Paragraph par)
            par.MoveAttributesTo(li);
          break;
        }
      }
      return element;
    }
  }
}
