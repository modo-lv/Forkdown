using System;
using Forkdown.Core.Elements;
using Simpler.NetCore;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Build.Builders {
  public class LinkIndexBuilder : Builder {
    private Document? _document;

    public LinkIndexBuilder() {
      this.MustRunAfter<LinesToParagraphsBuilder>();
      this.MustRunAfter<DocumentAttributesBuilder>();
      this.MustRunAfter<ExplicitIdBuilder>();
    }

    protected override TElement BuildElement<TElement>(TElement element, Maybe<Object> results) {
      if (element is Document doc)
        this._document = doc;

      var index = (LinkIndex)results.ValueOr(new LinkIndex());
      element.ExplicitIds.ForEach(_ =>
        index.Add(_, this._document!)
      );
      results.Value = index;
      
      return element;

    }
  }
}
