using System;
using Forkdown.Core.Main.Elements;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Forkdown.Core.Elements {
  public class Link : Container {
    public String Target;

    public Boolean IsInternal => this.Target.StartsWith("./");
    public Boolean IsExternal => !this.IsInternal;

    public Link(LinkInline link) : base(link) =>
      this.Target = link.Url;
  }
}
