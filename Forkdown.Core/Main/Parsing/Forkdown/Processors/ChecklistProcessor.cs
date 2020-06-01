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
    
    public void Process<T>(T element) where T : Element {
      _processId(element, Nil.DStr<Int32>());
      _processToggle(element);
    }

    private static void _processId<T>(T element, IDictionary<String, Int32> times, String parentId = "") where T : Element {
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
          times[id] = times.Get(id) + 1;
        if (times.Get(id) > 1)
          id += $"{R}{times[id]}";
        bc.CheckboxId = id;
      }
      
      element.Subs.ForEach(_ => _processId(_, times, id));
    }

    private static void _processToggle<T>(T element, Boolean inChecklist = false) where T : Element {
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

      if (element is Listing l)
        l.IsChecklist = inChecklist;
      
      element.Subs.ForEach(_ => _processToggle(_, inChecklist));
    }
  }
}
