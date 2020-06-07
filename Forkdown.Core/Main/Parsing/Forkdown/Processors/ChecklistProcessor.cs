using System;
using System.Collections.Generic;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  public class ChecklistProcessor : IElementProcessor{

    public T Process<T>(T element, IDictionary<String, Object> args) where T : Element {
      var inChecklist = (Boolean) args.GetOr("inChecklist", false);
      
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

      args["inChecklist"] = inChecklist;
      return element;
    }
  }
}
