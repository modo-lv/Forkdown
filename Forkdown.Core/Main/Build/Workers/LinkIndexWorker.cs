using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Build.Workers {
  public class LinkIndexWorker : IElementWorker {

    public T ProcessElement<T>(T element, Arguments args, Context context) where T : Element {
      var index = context.BuildStore.GetOrAdd(this.GetType(), new LinkIndex());
      element.GlobalIds.ForEach(_ =>
        index.Add(_, context.Document)
      );
      return element;
    }
  }
}
