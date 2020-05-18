using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Main.Elements;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Elements {
  public abstract class Element {
    public IList<Element> Subs = Nil.L<Element>();
    
    public String Title => Element.TitleOf(this);

    public readonly String Type;

    public ElementAttributes Attributes;


    protected Element(IMarkdownObject mdo) {
      this.Type = this.GetType().Name;
      this.Attributes = new ElementAttributes(mdo.GetAttributes());
    }


    private static String TitleOf(Element el) {
      return el switch
      {
        Text t => t.Content,
        _ => el.Subs.Where(sub => sub is Inline).Select(Element.TitleOf).StringJoin(),
      };
    }
  }
}