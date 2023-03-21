using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements.Attributes;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Elements; 

public abstract partial class Element {
  public enum Metadata {
    Normal, Info, Warning, Help
  }
  
  public IList<Element> Subs = Nil.L<Element>();

  // ReSharper disable once UnusedMember.Global
  public String Type => this.GetType().Name;

  public ElementSettings Settings => this.Attributes.Settings;

  public ElementAttributes Attributes = new ElementAttributes();

  public Boolean IsSingle;

  /// <summary>
  /// Element's project-unique identifier, either implicit or explicit.
  /// </summary>
  public String GlobalId => this.ExplicitId.NonBlank(this.ImplicitId)!;

  /// <summary>
  /// ID calculated from the item's (including its parents') <see cref="TitleText"/> and location in the document. 
  /// </summary>
  public String ImplicitId = "";

  /// <summary>
  /// Main identifier that is unique within the project. 
  /// </summary>
  public String ExplicitId => this.ExplicitIds.FirstOrDefault() ?? "";
    
  /// <summary>
  /// All project-unique identifiers for this element. 
  /// </summary>
  public IList<String> ExplicitIds = Nil.LStr;
  public Metadata Kind = Metadata.Normal;

  public virtual String TitleText => TitleTextOf(this);

    
  protected Element() {}

  protected Element(IMarkdownObject mdo) : this() {
    this.Attributes = new ElementAttributes(mdo.GetAttributes(), this);
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
    
  /// <summary>
  /// Move HTML attributes and Forkdown settings from this element to another.
  /// </summary>
  /// <param name="element">Target element.</param>
  public virtual void MoveAttributesTo(Element element) {
    element.Attributes = this.Attributes;
    element.ExplicitIds = this.ExplicitIds;
    this.Attributes = new ElementAttributes();
    this.ExplicitIds = Nil.LStr;
  }

}