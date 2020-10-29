using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements.Types;
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

    public Item(Element replaceElement) {
      this.ExplicitIds = replaceElement.ExplicitIds;
      this.Attributes = replaceElement.Attributes;
      this.Labels = replaceElement.Labels;
      this.Subs = replaceElement.Subs;
      this.ImplicitId = replaceElement.ImplicitId;
      this.IsSingle = replaceElement.IsSingle;
      this.Settings = replaceElement.Settings;

      if (this.Settings.IsTrue("-"))
        this.IsCheckitem = false;
      else if (this.Settings.IsTrue("+"))
        this.IsCheckitem = true;
      else if (this.Title is Paragraph p && p.Subs.FirstOrDefault() is Text t && t.Content.StartsWith(":# ")) {
        t.Content = t.Content.TrimPrefix(":# ");
        if (t.Content.IsBlank())
          this.Title.Subs.RemoveAt(0);
        this.IsCheckitem = false;
      }
      else {
        this.IsCheckitem = !(replaceElement is Heading || this.Subs.FirstOrDefault() is Heading);
      }
    }

    public static Item FromTitleElement(Element element) {
      var item = new Item();
      element.MoveAttributesTo(item);
      item.Subs.Add(element);
      item.IsHeading = element is Heading;
      return item;
    }
  }
}
