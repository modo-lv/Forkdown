using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Fd.Contexts;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Fd.Processors {
  public class GlobalIdProcessor : ITreeProcessor, IElementProcessor {
    public virtual Result<T> Process<T>(T element, IContext context) where T : Element {
      if (element.Attributes.Id.NotBlank()) {
        var ids = element.Attributes.Id.Split(',', StringSplitOptions.RemoveEmptyEntries);

        element.GlobalIds = ids
          .Select(_ => GlobalId.From(_.Replace(":id", element.Title)))
          .ToList();
      }
      return context.ToResult(element);
    }
  }
}
