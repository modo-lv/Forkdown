using System;
using System.Collections.Generic;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Forkdown.Core.Main.Elements {
  public class Link : Container {
    private String _target;

    public String Target
    {
      get => this._target;
      set
      {
        this._target = value;
        if (this.IsExternal)
        {
          this.Attributes.Classes.Remove("FD_internal");
          this.Attributes.Classes.Add("FD_external");
        }
        else
        {
          this.Attributes.Classes.Remove("FD_external");
          this.Attributes.Classes.Add("FD_internal");
        }
      }
    }

    public Boolean IsExternal => this.Target.Contains("//");

    public Link(MarkdownObject node) : base(node) {
      var link = (LinkInline) node;
      this.Target = link.Url;
    }
  }
}