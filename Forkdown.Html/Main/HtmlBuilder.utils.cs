using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Html.Main {
  /// <inheritdoc cref="HtmlBuilder"/>
  public partial class HtmlBuilder {
    /// <summary>
    /// Append Forkdown classes to output elements that need them.
    /// </summary>
    public static void ProcessClasses<T>(T element) where T : Element {
      switch (element) {
        case Listing l when l.IsChecklist:
          l.Attributes.Classes.Add("fd_checklist");
          break;
        case ListItem li when li.IsCheckbox:
          li.Attributes.Classes.Add("fd_checkbox");
          break;
        case Link l when l.IsInternal:
          l.Attributes.Classes.Add("fd_internal");
          break;
      }

      element.Subs.ForEach(ProcessClasses);
    }
  }
}
