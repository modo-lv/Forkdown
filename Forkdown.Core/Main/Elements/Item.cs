using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements.Types;
using Simpler.NetCore;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Elements {
  /// <summary>
  /// An item of information that can have metadata attached, contains sub-items etc.
  /// </summary>
  public class Item : BlockContainer {

    public Element Title => this.Subs.FirstOrDefault() ?? new Paragraph();

    public IEnumerable<Element> Content => this.Subs.SkipWhile(_ => _ == this.Title);

    public Boolean IsNewline = false;

    public Boolean HasHeading => this.Title is Heading;
    public Boolean IsHeading = false;

    public readonly Boolean IsCheckitem;

    public Item() { }

    public Item(Element replaceElement, ListingKind listKind) {
      this.ExplicitIds = replaceElement.ExplicitIds;
      this.Attributes = replaceElement.Attributes;
      this.Subs = replaceElement.Subs;
      this.Labels = this.Title?.Labels ?? this.Labels;
      this.Title.IfNotNull(t => t!.Labels = Nil.L<Label>());
      this.ImplicitId = replaceElement.ImplicitId;
      this.IsSingle = replaceElement.IsSingle;
      this.Settings = replaceElement.Settings;

      if (this.Settings.IsTrue("-"))
        this.IsCheckitem = false;
      else if (this.Settings.IsTrue("+"))
        this.IsCheckitem = true;
      else if (this.Settings.IsTrue("#") || this.Settings.IsTrue(" ")) {
        this.IsCheckitem = false;
      }
      else {
        this.IsCheckitem = listKind == ListingKind.CheckItems;
      }
    }

    public static Item FromTitleElement(Element element) {
      var item = new Item();
      element.MoveAttributesTo(item);
      item.Subs.Add(element);
      item.IsHeading = element is Heading;
      item.Labels = item.Title?.Labels ?? item.Labels;
      item.Title.IfNotNull(t => t!.Labels = Nil.L<Label>());
      return item;
    }
  }
}
