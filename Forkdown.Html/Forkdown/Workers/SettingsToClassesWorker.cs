using System;
using System.Collections.Generic;
using Forkdown.Core;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Simpler.NetCore.Text;

namespace Forkdown.Html.Main.Build {
  /// <summary>
  /// Decorates an element with CSS classes according to its settings. 
  /// </summary>
  public class SettingsToClassesWorker : Worker {

    /// <inheritdoc />
    public override T ProcessElement<T>(T element, Arguments args) {
      element.Attributes.Classes ??= new List<String>();

      if (element.Settings.ContainsKey("columns")) {
        element.Attributes.Classes.Add($"{Globals.Prefix}columns");
        if (element.Settings["columns"].NotBlank())
          element.Attributes.Classes.Add($"{Globals.Prefix}{element.Settings["columns"]}");
      }

      switch (element) {
        case Paragraph p when p.Kind != ParagraphKind.Normal:
          p.Attributes.Classes.Add($"{Globals.Prefix}{p.Kind.ToString().ToLowerInvariant()}");
          break;
        case Listing l when l.IsChecklist:
          l.Attributes.Classes.Add($"{Globals.Prefix}checklist");
          break;
        case ListItem li when li.IsCheckitem:
          li.Attributes.Classes.Add($"{Globals.Prefix}checkitem");
          break;
        case Link l when l.IsInternal:
          l.Attributes.Classes.Add($"{Globals.Prefix}internal");
          break;
      }
      return element;
    }
  }
}
