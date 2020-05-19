using System;
using Forkdown.Core.Main.Elements;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Forkdown.Core.Elements {
  public class Link : Container {
    public String Target;

    public Boolean IsInternal => !this.IsExternal;
    public Boolean IsExternal => this.Target.Contains("//");

    public Link(LinkInline link) : base(link) =>
      this.Target = link.Url;
  }
}
