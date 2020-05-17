using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Markdig.Syntax;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Main.Elements {
  public abstract class Element {
    public IList<Element> Subs = new List<Element>();
    
    public String Title => Element.TitleOf(this);

    public readonly String Type;


    protected Element(MarkdownObject node) {
      this.Type = this.GetType().Name;
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