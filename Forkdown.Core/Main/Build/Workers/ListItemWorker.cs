using System.Linq;
using Forkdown.Core.Elements;

namespace Forkdown.Core.Build.Workers {
  public class ListItemWorker : Worker {
    public ListItemWorker() {
      this.RunsAfter<LabelWorker>();
    }
    
    public override TElement BuildElement<TElement>(TElement element) {
      if (element is ListItem li && li.Subs.Any()) {
        li.Subs.First().MoveAttributesTo(li);
      }
      return element;
    }
  }
}
