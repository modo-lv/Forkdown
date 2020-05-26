using System;
using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Types;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  public class LabelProcessor : IForkdownProcessor {

    public T Process<T>(T element, Document? doc = null) where T : Element {
      if (element is Block && element.Subs.Any() && element.Subs[0] is Code labels) {
        element.Subs.RemoveAt(0);

        element.Labels = labels
          .Content
          .Split(' ', ',', StringSplitOptions.RemoveEmptyEntries)
          .Select(_ => _.Trim())
          .ToHashSet();

        if (element.Subs.Any() && element.Subs[0] is Text t)
          t.Content = t.Content.TrimStart();
      }
      element.Subs.ForEach(_ => Process(_));
      return element;
    }
  }
}
