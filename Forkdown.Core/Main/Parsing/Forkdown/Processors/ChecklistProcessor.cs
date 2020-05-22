using System;
using System.Collections.Generic;
using Forkdown.Core.Elements;
using Simpler.Net;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  public class ChecklistProcessor : IForkdownProcessor {
    public const Char G = '␝'; // Group separator
    public const Char R = '␞'; // Record separator
    public const Char W = '⸱'; // Word separator

    public T Process<T>(T element, Document? doc = null) where T : Element {
      return proc(element, Nil.DStr<Int32>(), doc);
      static TEl proc<TEl>(
        TEl el,
        IDictionary<String, Int32> times,
        Document? doc = null,
        String parentId = ""
      ) where TEl : Element {

        String id = "";
        if (el is Document dEl) {
          id = dEl.FileName;
          doc = dEl;
        }
        else if (el.GlobalId.NotBlank()) {
          id = doc!.FileName.NotBlank() ? $"{doc!.FileName}{G}{el.GlobalId}" : el.GlobalId;
        }
        else if (el is Section sec) {
          id = sec.Title.Replace(' ', W);
          if (parentId.NotBlank())
            id = $"{parentId}{G}{id}";
          if (id.NotBlank())
            times[id] = times.Get(id) + 1;
          if (times.Get(id) > 1)
            id += $"{R}{times[id]}";
          sec.CheckboxId = id;
        }
        else
          id = parentId;

        el.Subs.ForEach(_ => proc(_, times, doc, id));

        return el;
      }
    }
  }
}
