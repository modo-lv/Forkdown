using System;
using Forkdown.Core.Elements;

namespace Forkdown.Core.Build.Workers {
  public class ChecklistWorker : Worker {

    public override T ProcessElement<T>(T element, Arguments args) {
      var inChecklist = args.Get<Boolean>();
      
      if (element is ListItem item) {
        if (inChecklist)
          item.IsCheckitem = item.Settings.NotFalse("checkbox");
        
        item.IsCheckitem = item.BulletChar switch {
          '+' => true,
          '-' => false,
          _ => item.IsCheckitem
        };
      }

      if (element.Settings.IsTrue("checklist"))
        inChecklist = true;
      else if (element.Settings.IsFalse("checklist"))
        inChecklist = false;

      if (element is Listing l)
        l.IsChecklist = inChecklist;

      args.Put(inChecklist);
      return element;
    }
  }
}
