using Forkdown.Core.Elements;
using MoreLinq.Extensions;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Build.Workers {
  /// <summary>
  /// Must be run after <see cref="ExplicitIdWorker"/>.
  /// </summary>
  public class LinkIndexWorker : Worker, IProjectWorker {

    private Document? _document;

    public override Element ProcessElement(Element element, Arguments args) {
      switch (element) {
        case IdScope _: return element;
        case Document doc: this._document = doc;
          break;
      }

      var index = this.Builder!.Storage.GetOrAdd(this.GetType(), new LinkIndex());
      CollectionExtensions.ForEach(element.ExplicitIds, _ =>
        index.Add(_, this._document!)
      );
      
      return element;
    }
  }
}
