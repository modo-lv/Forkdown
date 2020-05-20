using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements.Types;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Simpler.NetCore.Collections;

// ReSharper disable NotAccessedField.Global

namespace Forkdown.Core.Elements {
  public abstract class Element {
    public IList<Element> Subs = Nil.L<Element>();

    public String Title => Element.TitleOf(this);

    public readonly String Type;

    public readonly ElementAttributes Attributes;

    /// <summary>
    /// Keyword(s) that 
    /// </summary>
    public String Anchor = "";


    protected Element(IMarkdownObject mdo) {
      this.Type = this.GetType().Name;
      this.Attributes = new ElementAttributes(mdo.GetAttributes());
    }


    private static String TitleOf(Element el, Boolean contentFound = false) {
      if (el is Text t)
        return t.Content;

      if (contentFound) {
        return el.Subs.Where(_ => _ is Text || _ is Inline)
          .Select(_ => Element.TitleOf(_, contentFound))
          .StringJoin();
      }
      
      return el.Subs.Take(1)
        .Select(_ => TitleOf(_, _ is Inline))
        .FirstOrDefault() ?? "";
    }
  }
}
