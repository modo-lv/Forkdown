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

    public virtual String Title => Element.TitleOf(this);

    public String Type => this.GetType().Name;

    public ElementSettings Settings = new ElementSettings();

    public ElementAttributes Attributes;

    public ISet<String> Labels = Nil.SStr;

    /// <summary>
    /// Main identifier that is unique within the project. 
    /// </summary>
    public String GlobalId => this.GlobalIds.FirstOrDefault() ?? "";
    /// <summary>
    /// All project-unique identifiers 
    /// </summary>
    public IList<String> GlobalIds = Nil.LStr;

    protected Element() {
      this.Attributes = new ElementAttributes(new HtmlAttributes(), this.Settings);
    }

    protected Element(IMarkdownObject mdo) : this() {
      this.Attributes = new ElementAttributes(mdo.GetAttributes(), this.Settings);

      if (this.Settings.ContainsKey("grid")) {
        this.Attributes.Classes.Add($"{Globals.Prefix}grid");
        if (this.Settings["grid"].NotBlank())
          this.Attributes.Classes.Add($"{Globals.Prefix}{this.Settings["grid"]}");
      }
      else if (this.Settings.ContainsKey("columns")) {
        this.Attributes.Classes.Add($"{Globals.Prefix}columns");
        if (this.Settings["columns"].NotBlank())
          this.Attributes.Classes.Add($"{Globals.Prefix}{this.Settings["columns"]}");
      }
    }


    public T? Find<T>() where T : Element {
      return (this.Subs.FirstOrDefault(_ => _ is T)
              ?? this.Subs.Select(_ => _.Find<T>()).FirstOrDefault(_ => _ != null))
        as T;
    }
  }
}
