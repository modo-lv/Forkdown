using System;
using System.Collections.Generic;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

// ReSharper disable NotAccessedField.Global

namespace Forkdown.Core.Elements {
  public abstract partial class Element {
    public IList<Element> Subs = Nil.L<Element>();

    public String Title => Element.TitleOf(this);

    public readonly String Type = "";

    public ElementAttributes Attributes = new ElementAttributes(new HtmlAttributes());

    private String _globalId = "";
    /// <summary>
    /// An identifier that is unique within the project. 
    /// </summary>
    public String GlobalId {
      get => this._globalId;
      set {
        this._globalId = value;
        if (value.NotBlank())
          this.Attributes.Id = value;
      }
    }

    protected Element() { }

    protected Element(IMarkdownObject mdo) {
      this.Type = this.GetType().Name;
      this.Attributes = new ElementAttributes(mdo.GetAttributes());
    }
  }
}
