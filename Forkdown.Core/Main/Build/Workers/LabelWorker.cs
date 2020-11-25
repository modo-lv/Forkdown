using System;
using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Types;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Build.Workers {
  public class LabelWorker : Worker {
    public LabelWorker() {
      this.RunsAfter<LinesToParagraphsWorker>();
    }

    public override TElement BuildElement<TElement>(TElement element) {
      // Code labels
      if (element is Block && element.Subs.FirstOrDefault() is Code labelCode) {
        element.Subs.RemoveAt(0);

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
            var lc = this.Config?.Labels?.All.GetOr(key, null!);
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
