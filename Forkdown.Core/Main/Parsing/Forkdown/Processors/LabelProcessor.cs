using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Types;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  public class LabelProcessor : IDocumentProcessor{

    public T Process<T>(T element, IDictionary<String, Object> args) where T : Element {
      if (element is Block && element.Subs.Any() && element.Subs[0] is Code labels) {
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
