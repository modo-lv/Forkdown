using System.Linq;
using Forkdown.Core.Elements;

namespace Forkdown.Core.Build.Workers {
  public class ListItemWorker : Worker {

    public override Element ProcessElement(Element element, Arguments args) {
      switch (element) {
        case Listing list:
          args.Put(list.BulletChar);
          break;
        case ListItem li: {
          li.BulletChar = args.GetOr('*');
          break;
        }
      }
      return element;
    }
  }
}
