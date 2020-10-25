﻿using System;
using System.Collections.Generic;
using Forkdown.Core;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Simpler.NetCore.Text;

namespace Forkdown.Html.Forkdown.Workers {
  /// <summary>
  /// Decorates an element with CSS classes according to its settings. 
  /// </summary>
  public class SettingsToClassesWorker : Worker {

    /// <inheritdoc />
    public override Element ProcessElement(Element element, Arguments args) {
      element.Attributes.Classes ??= new List<String>();
      
      if (element.Settings.IsTrue("-"))
        element.Attributes.Classes.Add($"{Globals.Prefix}inline");

      if (element.Settings.ContainsKey("columns")) {
        element.Attributes.Classes.Add($"{Globals.Prefix}columns");
        if (element.Settings["columns"].NotBlank())
          element.Attributes.Classes.Add($"{Globals.Prefix}{element.Settings["columns"]}");
      }

      if (element.IsSingle) {
        element.Attributes.Classes.Add($"{Globals.Prefix}single");
        element.Attributes.AddProperty($"data-{Globals.Prefix}single-id", Globals.Id(element.TitleText));
      }

      if (element is Item i) {
        element.Attributes.Classes.Add($"{Globals.Prefix}item");
        if (i.IsCheckitem)
          element.Attributes.Classes.Add($"{Globals.Prefix}checkitem");
        if (i.HasHeading || i.IsHeading) {
          if (i.IsHeading)
            element.Attributes.Classes.Add($"{Globals.Prefix}is-heading");
          else if (i.HasHeading)
            element.Attributes.Classes.Add($"{Globals.Prefix}has-heading");
          element.Attributes.Classes.Add($"{Globals.Prefix}h{((Heading) i.Title).Level}");
        }
        if (i.IsNewline) {
          element.Attributes.Classes.Add($"{Globals.Prefix}newline");
        }
      }

      switch (element) {
        case Paragraph p:
          if (p.Kind != ParagraphKind.Normal)
            p.Attributes.Classes.Add($"{Globals.Prefix}{p.Kind.ToString().ToLowerInvariant()}");
          if (p.IsTitle)
            p.Attributes.Classes.Add($"{Globals.Prefix}title");
          break;
        case Link l when l.IsInternal:
          l.Attributes.Classes.Add($"{Globals.Prefix}internal");
          break;
      }
      return element;
    }
  }
}
