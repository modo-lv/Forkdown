using System;
using Markdig.Syntax.Inlines;
using Inline = Forkdown.Core.Elements.Types.Inline;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Forkdown.Core.Elements {
  public class Link : Element, Inline {
    public String Target;

    public Boolean IsInternal => !this.IsExternal;
    public Boolean IsExternal => this.Target.Contains("//");
    
    public Link(LinkInline link) : base(link) =>
      this.Target = link.Url;
  }
}
