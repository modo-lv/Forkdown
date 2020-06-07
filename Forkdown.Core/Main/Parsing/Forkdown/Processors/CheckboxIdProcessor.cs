using System;
using System.Collections.Generic;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Types;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  public class CheckboxIdProcessor : IElementProcessor{
    public const Char G = '␝'; // Group separator
    public const Char R = '␞'; // Record separator
    public const Char W = '⸱'; // Word separator

    protected readonly IDictionary<String, Int32> Times = Nil.DStr<Int32>();

    public T Process<T>(T element, IDictionary<String, Object> args) where T : Element {
      var parentId = (String) args.GetOr("parentId", "");
      var times = this.Times;
      var id = parentId;
      
      if (element is Document dEl) {
        id = dEl.ProjectFileId;
      }
      else if (element.GlobalId.NotBlank()) {
        id = element.GlobalId;
      }
      else if (element is BlockContainer bc) {
        id = element.Title.Replace(' ', W);
        if (parentId.NotBlank())
          id = $"{parentId}{G}{id}";
        if (id.NotBlank())
          times[id] = times.GetOr(id, 0) + 1;
        if (times.GetOr(id, 0) > 1)
          id += $"{R}{times[id]}";
        bc.CheckboxId = id;
      }

      args["parentId"] = id;
      return element;
    }
  }
}
