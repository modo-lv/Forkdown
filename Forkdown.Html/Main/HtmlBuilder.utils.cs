using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Html.Main {
  public partial class HtmlBuilder {
    public static void ProcessClasses<T>(T element) where T : Element {
      switch (element) {
        case Listing l when l.IsChecklist:
          l.Attributes.Classes.Add("fd_checklist");
          break;
        case ListItem li when li.IsCheckbox:
          li.Attributes.Classes.Add("fd_checkbox");
          break;
      }

      element.Subs.ForEach(ProcessClasses);
    }
  }
}
