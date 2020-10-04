using System.Linq;
using Forkdown.Core.Elements;

namespace Forkdown.Core.Build.Workers {
  public class ListItemWorker : Worker {

    public override Element ProcessElement(Element element, Arguments args) {
      if (element is ListItem li && li.Subs.FirstOrDefault() is { } sub) {
        sub.MoveAttributesTo(li);
      }
      return element;
    }
  }
}
