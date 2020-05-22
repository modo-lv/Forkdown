using System;
using System.Linq;
using Forkdown.Core.Elements.Types;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Elements {
  public partial class Element {
    private static String TitleOf(Element el, Boolean contentFound = false) {
      if (el is Text t)
        return t.Content;

      if (contentFound) {
        return el.Subs.Where(_ => _ is Inline)
          .Select(_ => Element.TitleOf(_, contentFound))
          .StringJoin();
      }

      return el.Subs.Take(1)
        .Select(_ => TitleOf(_, _ is Inline))
        .FirstOrDefault() ?? "";
    }
  }
}
