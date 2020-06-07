using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  public class GlobalIdProcessor : IDocumentProcessor {
    public T Process<T>(T element, IDictionary<String, Object> args) where T : Element {
      if (element.Attributes.Id.NotBlank()) {
        var ids = element.Attributes.Id.Split(',', StringSplitOptions.RemoveEmptyEntries);

        element.GlobalIds = ids
          .Select(_ => GlobalId.From(_.Replace(":id", element.Title)))
          .ToList();
      }
      return element;
    }
  }
}
