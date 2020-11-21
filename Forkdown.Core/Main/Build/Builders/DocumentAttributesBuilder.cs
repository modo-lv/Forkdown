using System;
using System.Linq;
using Forkdown.Core.Elements;
using Simpler.NetCore;

namespace Forkdown.Core.Build.Builders {
  public class DocumentAttributesBuilder : Builder {

    public DocumentAttributesBuilder() {
      this.MustRunAfter<ExplicitIdBuilder>();
    }

    public override TElement BuildTree<TElement>(TElement root, Maybe<Object> objects) {
      if (!(root is Document))
        return root;
      if (root.Subs.FirstOrDefault() is Paragraph par &&
          par.Subs.FirstOrDefault() is Text text &&
          text.Content == ":") {
        par.MoveAttributesTo(root);
      }
      return root;
    }
  }
}
