using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Build.Workers {
  public class LinkIndexWorker : Worker {
    private Document? _document;

    public LinkIndexWorker() {
      this.RunsAfter<LinesToParagraphsWorker>();
      this.RunsAfter<DocumentAttributesWorker>();
      this.RunsAfter<ExplicitIdWorker>();
    }

    public override Element BuildElement(Element element) {
      var index = this.Stored(new LinkIndex());
      if (element is Document doc)
        this._document = doc;

      element.ExplicitIds.ForEach(id =>
        index.Add(id, this._document!)
      );

      return element;

    }
  }
}
