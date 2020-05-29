using System;
using System.Collections.Generic;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Types;
using Simpler.Net;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  public class ChecklistProcessor : IForkdownProcessor {
    public const Char G = '␝'; // Group separator
    public const Char R = '␞'; // Record separator
    public const Char W = '⸱'; // Word separator

    public T ProcessElement<T>(T element, IDictionary<String, Object> context) where T : Element {
      _processId(element, context);
      _processToggle(element, context);
      return element;
    }

    private static void _processId<T>(T element, IDictionary<String, Object> context) where T : Element {
      var times = (IDictionary<String, Int32>)context.GetOrAdd("times", Nil.DStr<Int32>());
      var parentId = (String)context.GetOrAdd("parentId", "");
      var id = parentId;
      
      if (element is Document dEl) {
        id = dEl.ProjectFileId;
      }
      else if (element.GlobalId.NotBlank()) {
        id = element.GlobalId;
      }
      else if (element is BlockContainer bc) {
        id = bc.Title.Replace(' ', W);
        if (parentId.NotBlank())
          id = $"{parentId}{G}{id}";
        if (id.NotBlank())
          times[id] = times.Get(id) + 1;
        if (times.Get(id) > 1)
          id += $"{R}{times[id]}";
        bc.CheckboxId = id;
      }

      context["parentId"] = id;
    }

    private static void _processToggle<T>(T element, IDictionary<String, Object> context) where T : Element {
      var inChecklist = (Boolean) context.GetOrAdd("inChecklist", false);
      
      if (element is ListItem item) {
        if (inChecklist)
         item.IsCheckbox = item.Settings.NotFalse("checkbox");
        
        item.IsCheckbox = item.BulletType switch {
          '+' => true,
          '-' => false,
          _ => item.IsCheckbox
        };
      }

      if (element.Settings.IsTrue("checklist"))
        inChecklist = true;
      else if (element.Settings.IsFalse("checklist"))
        inChecklist = false;

      element.IsChecklist = inChecklist;

      context["inChecklist"] = inChecklist;
    }
  }
}
