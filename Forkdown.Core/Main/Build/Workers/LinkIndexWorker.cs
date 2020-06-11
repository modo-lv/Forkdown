using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Build.Workers {
  public class LinkIndexWorker : Worker, IProjectWorker {

    private Document? _document;

    public override T ProcessElement<T>(T element, Arguments args) {
      if (element is Document doc)
        this._document = doc;

      var index = this.Builder!.Storage.GetOrAdd(this.GetType(), new LinkIndex());
      element.GlobalIds.ForEach(_ =>
        index.Add(_, this._document!)
      );
      
      return element;
    }
  }
}
