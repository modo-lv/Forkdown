using System;
using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Fd.Contexts;

namespace Forkdown.Core.Fd.Processors {
  public class ListItemProcessor : IElementProcessor {

    public virtual Result<T> Process<T>(T element, IContext context) where T : Element {
      switch (element) {
        case Listing list:
          context.SetArg(list.BulletChar);
          break;
        case ListItem li: {
          li.BulletChar = context.GetArg('*');
          if (li.Subs.FirstOrDefault() is Paragraph par)
            par.MoveAttributesTo(li);
          break;
        }
      }
      return context.ToResult(element);
    }
  }
}
