using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Parsing.Forkdown.OldProcessors {
  /*
  /// <summary>
  /// Parse an element's global ID.
  /// </summary>
  public class GlobalIdProcessor : IForkdownProcessor {
    public void Process<T>(T element) where T : Element {
      element.GlobalIds = element
        .HtmlAttributes.Classes.ToList()
        .Prepend(element.HtmlAttributes.Id)
        .Where(_ => _.StartsWith("#") || _ == "#~")
        .Select(_ => {
          element.HtmlAttributes.Classes.Remove(_);
          return GlobalId.From(_ == "#~" ? element.Title : _.Part(1));
        })
        .ToList();
      if (element.GlobalId.NotBlank())
        element.HtmlAttributes.Id = element.GlobalId;

      element.Subs.ForEach(this.Process);
    }
  }
  */
}
