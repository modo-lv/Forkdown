using System;
using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Types;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Build.Workers {
  public class LabelWorker : Worker {

    public override Element ProcessElement(Element element, Arguments args) {
      // Code labels
      if (element is Block && element.Subs.FirstOrDefault() is Code labels) {
        element.Subs.RemoveAt(0);

        element.Labels = labels
          .Content
          .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
          .Select(_ => _.Trim())
          .ToHashSet();

        if (element.Subs.Any() && element.Subs[0] is Text t)
          t.Content = t.Content.TrimStart();
      }
     
      return element;
    }
  }
}
