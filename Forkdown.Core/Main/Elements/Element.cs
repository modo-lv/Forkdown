using System;
using System.Collections.Generic;
using System.Linq;
using Markdig.Syntax;

namespace Forkdown.Core.Main.Elements {
  public abstract class Element {
    
    public IList<Element> Subs = new List<Element>();

    public String Title => Element.TitleOf(this);
    

    
    protected Element(MarkdownObject node) { }

    

    private static String TitleOf(Element el) {
      return el switch
      {
        Text t => t.Content,
        _ => el.Subs.Where(sub => sub is Inline).Select(Element.TitleOf).Aggregate(String.Join)
      };
    }
    
  }
}