using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Types;
using Simpler.NetCore.Collections;

/*
namespace Forkdown.Core.Parsing.Forkdown.OldProcessors {
  public class LabelProcessor : IForkdownProcessor {

    public void Process<T>(T element) where T : Element {
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
      element.Subs.ForEach(this.Process);
    }
  }
}
*/
