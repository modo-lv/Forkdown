using System;
using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Fd.Contexts;

namespace Forkdown.Core.Fd.Processors {
  public class ListItemProcessor : IElementProcessor {

    public virtual T ProcessElement<T>(T element, Arguments args, IContext context) where T : Element {
      switch (element) {
        case Listing list:
          args.Put(list.BulletChar);
          break;
        case ListItem li: {
          li.BulletChar = args.GetOr('*');
          if (li.Subs.FirstOrDefault() is Paragraph par)
            par.MoveAttributesTo(li);
          break;
        }
      }
      return element;
    }
  }
}
