using System;
using Forkdown.Core;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Html.Main {
  /// <inheritdoc cref="HtmlBuilder"/>
  public partial class HtmlBuilder {
    /// <summary>
    /// Append Forkdown classes to output elements that need them.
    /// </summary>
    public static void ProcessClasses<T>(T element) where T : Element {
      throw new NotImplementedException();
      /*switch (element) {
        case Listing l when l.IsChecklist:
          l.HtmlAttributes.Classes.Add($"{Globals.Prefix}checklist");
          break;
        case ListItem li when li.IsCheckbox:
          li.HtmlAttributes.Classes.Add($"{Globals.Prefix}checkbox");
          break;
        case Link l when l.IsInternal:
          l.HtmlAttributes.Classes.Add($"{Globals.Prefix}internal");
          break;
      }

      element.Subs.ForEach(ProcessClasses);*/
    }
  }
}
