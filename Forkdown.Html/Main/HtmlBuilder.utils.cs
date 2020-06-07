using System;
using System.Collections.Generic;
using Forkdown.Core;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Html.Main {
  /// <inheritdoc cref="HtmlBuilder"/>
  public partial class HtmlBuilder {
    /// <summary>
    /// Append Forkdown classes to output elements that need them.
    /// </summary>
    public static void ProcessClasses<T>(T element) where T : Element {
      element.Attributes.Classes ??= new List<String>();
      
      if (element.Settings.ContainsKey("columns")) {
        element.Attributes.Classes.Add($"{Globals.Prefix}columns");
        if (element.Settings["columns"].NotBlank())
          element.Attributes.Classes.Add($"{Globals.Prefix}{element.Settings["columns"]}");
      }
      
      switch (element) {
        case Listing l when l.IsChecklist:
          l.Attributes.Classes.Add($"{Globals.Prefix}checklist");
          break;
        case ListItem li when li.IsCheckbox:
          li.Attributes.Classes.Add($"{Globals.Prefix}checkbox");
          break;
        case Link l when l.IsInternal:
          l.Attributes.Classes.Add($"{Globals.Prefix}internal");
          break;
      }

      element.Subs.ForEach(ProcessClasses);
    }
  }
}
