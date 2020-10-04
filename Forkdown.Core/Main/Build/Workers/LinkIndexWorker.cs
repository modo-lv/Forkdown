using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Build.Workers {
  /// <summary>
  /// Must be run after <see cref="ExplicitIdWorker"/>.
  /// </summary>
  public class LinkIndexWorker : Worker, IProjectWorker {

    private Document? _document;

    public override Element ProcessElement(Element element, Arguments args) {
      if (element is Document doc)
        this._document = doc;

      var index = this.Builder!.Storage.GetOrAdd(this.GetType(), new LinkIndex());
      element.ExplicitIds.ForEach(_ =>
        index.Add(_, this._document!)
      );
      
      return element;
    }
  }
}
