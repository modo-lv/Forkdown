using Forkdown.Core.Elements;

namespace Forkdown.Core.Build.Workers {
  public class LinkWorker : IElementWorker {

    public virtual T ProcessElement<T>(T element, Arguments args, Context context) where T : Element {
      if (element is Link link && link.Target == "@") {
        link.Target = $"@{link.Title}";
      }
      
      return element;
    }
  }
}
