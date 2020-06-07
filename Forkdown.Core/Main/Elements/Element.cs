using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements.Attributes;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

// ReSharper disable NotAccessedField.Global

namespace Forkdown.Core.Elements {
  public abstract partial class Element {
    public IList<Element> Subs = Nil.L<Element>();

    public virtual String Title => Element.TitleOf(this);

    public String Type => this.GetType().Name;

    public ElementSettings Settings = new ElementSettings();

    /// <summary>
    /// Arbitrary data attached to the element.
    /// </summary>
    public IDictionary<String, Object> Data = Nil.DStr<Object>();

    public ISet<String> Labels = Nil.SStr;

    /// <summary>
    /// Main identifier that is unique within the project. 
    /// </summary>
    public String GlobalId => this.GlobalIds.FirstOrDefault() ?? "";
    /// <summary>
    /// All project-unique identifiers for this element. 
    /// </summary>
    public IList<String> GlobalIds = Nil.LStr;

    
    
    protected Element() {}

    protected Element(params Element[] subs) : this() {
      this.Subs = subs;
    }

    protected Element(IMarkdownObject mdo) : this() {
      this.Data[Globals.HtmlDataKey] = mdo.GetAttributes();

      /*
      if (this.Settings.ContainsKey("columns")) {
        this.Attributes.Classes.Add($"{Globals.Prefix}columns");
        if (this.Settings["columns"].NotBlank())
          this.Attributes.Classes.Add($"{Globals.Prefix}{this.Settings["columns"]}");
      }
    */
    }


    public virtual Element AddSub(Element sub) {
      this.Subs.Add(sub);
      return this;
    }

    public virtual Element AddSubs(params Element[] subs) {
      subs.ForEach(this.Subs.Add);
      return this;
    }
    
    public T FirstSub<T>() where T : Element =>
      this.FindSub<T>()!;

    public T? FindSub<T>() where T : Element {
      return (this.Subs.FirstOrDefault(_ => _ is T)
              ?? this.Subs.Select(_ => _.FindSub<T>()).FirstOrDefault(_ => _ != null))
        as T;
    }
  }
}
