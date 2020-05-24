using System;
using System.Collections.Generic;
using System.Linq;
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

    public ElementSettings Settings = new ElementSettings();

    public ElementAttributes Attributes;

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

    protected Element() {
      this.Attributes = new ElementAttributes(new HtmlAttributes(), this.Settings);
    }

    protected Element(IMarkdownObject mdo) : this() {
      this.Type = this.GetType().Name;
      this.Attributes = new ElementAttributes(mdo.GetAttributes(), this.Settings);
    }


    public T? Find<T>() where T : Element {
      return (this.Subs.FirstOrDefault(_ => _ is T)
              ?? this.Subs.Select(_ => _.Find<T>()).FirstOrDefault(_ => _ != null))
        as T;
    }
  }
}
