using System;
using System.Linq;
using Forkdown.Core.Elements.Types;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Elements {
  public partial class Element {
    protected static String TitleOf(Element el, Boolean contentFound = false) {
      if (el is Text t)
        return t.Content;

      if (contentFound) {
        return el.Subs.TakeWhile(_ => _ is Inline && !(_ is LineBreak))
          .Select(_ => Element.TitleOf(_, contentFound))
          .StringJoin();
      }

      return (el.Subs.FirstOrDefault() is Inline inline && inline != null
               ? TitleOf(el, true)
               : el.Subs.Take(1)
                 .Select(_ => TitleOf(_, false))
                 .FirstOrDefault())
             ?? "";
    }
  }
}
