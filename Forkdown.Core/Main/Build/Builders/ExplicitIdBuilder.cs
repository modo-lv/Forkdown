using System;
using System.Collections.Generic;
using System.Linq;
using Simpler.NetCore;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Build.Builders {
  public class ExplicitIdBuilder : Builder {
    public ExplicitIdBuilder() {
      this.MustRunAfter<LinesToParagraphsBuilder>();
    }

    protected override TElement BuildElement<TElement>(TElement element, Maybe<Object> objects) {
      if (element.ExplicitIds.FirstOrDefault().NotBlank()) {
        var ids = element.ExplicitIds.FirstOrDefault().Split(',', StringSplitOptions.RemoveEmptyEntries);

        element.ExplicitIds = ids
          .Select(_ => Globals.Id(_.Replace(":id", element.TitleText)))
          .ToList();
      }
      return element;
    }
  }
}
