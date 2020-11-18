using System;
using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Types;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Build.Workers {
  public class LabelWorker : Worker {

    public override Element ProcessElement(Element element, Arguments args) {
      // Code labels
      if (element is Block && element.Subs.FirstOrDefault() is Code labelCode) {
        if (element.Settings.Any()) {
          throw new Exception(
            $"{nameof(LabelWorker)} must be run before any other workers that update an element's Settings."
          );
        }

        element.Subs.RemoveAt(0);

        if (labelCode.Content == " ")
          element.Settings[" "] = "true";

        var labelStrings = labelCode
          .Content
          .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
          .Select(_ => _.Trim())
          .ToHashSet();
        
        // Settings
        labelStrings
          .Where(key => key.StartsWith(":"))
          .ForEach(key => element.Settings[key.Substring(1)] = "");

        // Regular labels
        element.Labels = labelStrings
          .Where(_ => !_.StartsWith(":"))
          .Select(key => {
            var result = new Label(key);
            var lc = this.Builder?.LabelsConfig?.All.GetOr(key, null!);
            if (lc != null) {
              result.Name = lc!.Name.NonBlank() ?? result.Name;
              result.Icon = lc!.Icon;
            }
            return result;
          })
          .ToList()!;

        if (element.Subs.FirstOrDefault() is Text t)
          t.Content = t.Content.TrimStart();
      }

      return element;
    }
  }
}
