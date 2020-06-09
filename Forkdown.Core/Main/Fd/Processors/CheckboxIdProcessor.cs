using System;
using System.Collections.Generic;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Types;
using Forkdown.Core.Fd.Contexts;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Fd.Processors {
  public class CheckboxIdProcessor : IElementProcessor {
    public const Char G = '␝'; // Group separator
    public const Char R = '␞'; // Record separator
    public const Char W = '⸱'; // Word separator

    public virtual T ProcessElement<T>(T element, Arguments args, IContext context) where T : Element {
      String parentId = args.Get();
      var times = context.Doc<Dictionary<String, Int32>>();
      
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

      args.Put(id);
      return element;
    }


  }
}
