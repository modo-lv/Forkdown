using System;
using System.Linq;
using Forkdown.Core.Elements;

namespace Forkdown.Core.Build.Workers {
  public class ExplicitIdWorker : Worker {
    public ExplicitIdWorker() {
      this.RunsAfter<LinesToParagraphsWorker>();
      this.RunsAfter<ListItemWorker>();
    }

    public override Element BuildElement(Element element) {
      if (element.ExplicitIds.Any()) {
        var ids = element.ExplicitIds.FirstOrDefault().Split(',', StringSplitOptions.RemoveEmptyEntries);

        element.ExplicitIds = ids
          .Select(_ => Globals.Id(_.Replace(":id", element.TitleText)))
          .ToList();
      }
      return element;
    }
  }
}
