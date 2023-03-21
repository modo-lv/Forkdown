using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements.Markdown;
using Forkdown.Core.Elements.Types;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Elements; 

/// <summary>
/// An item of information that can have metadata attached, contains sub-items etc.
/// </summary>
public class Item : BlockContainer {

  public Element Title => this.Subs.FirstOrDefault() ?? new Paragraph();

  public IEnumerable<Element> Content => this.Subs.SkipWhile(_ => _ == this.Title);

  public Boolean HasHeading => this.Title is Heading;
  public Boolean IsHeading;
  public IList<Tip> Labels = Nil.L<Tip>();

  public readonly Boolean IsCheckitem;

  public Item() { }

  public Item(Element replaceElement, ListingKind listKind) {
    this.ExplicitIds = replaceElement.ExplicitIds;
    this.Attributes = replaceElement.Attributes;
    this.Subs = replaceElement.Subs;
    this.Labels = this.Title.Subs.OfType<Tip>().ToList();
    this.Title.Subs = this.Title.Subs.Except(this.Labels).ToList();
    this.ImplicitId = replaceElement.ImplicitId;
    this.IsSingle = replaceElement.IsSingle;
    this.IsCheckitem = listKind == ListingKind.CheckItems;
  }

  public static Item FromTitleElement(Element element) {
    var item = new Item();
    element.MoveAttributesTo(item);
    item.Subs.Add(element);
    item.IsHeading = element is Heading;
    item.Labels = item.Title.Subs.OfType<Tip>().ToList();
    item.Title.Subs = item.Title.Subs.Except(item.Labels).ToList();
    return item;
  }
}