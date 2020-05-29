using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  /// <summary>
  /// Parse an element's global ID.
  /// </summary>
  public class GlobalIdProcessor : IForkdownProcessor {
    public void Process<T>(T element) where T : Element {
      element.Attributes.Classes.ToList().Prepend(element.Attributes.Id)
        .Where(_ => _.StartsWith("#") || _ == "#~")
        .Select(_ => {
          element.Attributes.Classes.Remove(_);
          return _;
        })
        .ToList()
        .Take(1)
        .ForEach(a => element.GlobalId = GlobalId.From(a == "#~" ? element.Title : a.Part(1)));
      
      element.Subs.ForEach(this.Process);
    }
  }
}
