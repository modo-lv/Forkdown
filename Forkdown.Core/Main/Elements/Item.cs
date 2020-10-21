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
    public IEnumerable<Element> Contents => this.Subs.SkipWhile(_ => _ == this.Title);
    
    public Boolean IsNewline = false;

    public Boolean IsHeading => this.Title is Heading;

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

      this.IsCheckitem = !(this.Subs.FirstOrDefault() is Heading);
      if (this.Title is Paragraph p && p.Subs.FirstOrDefault() is Text t && t.Content.StartsWith(":- ")) {
        t.Content = t.Content.TrimPrefix(":- ");
        this.IsCheckitem = false;
      }
    }
    
    public static Item Wrap(IList<Element> elements) {
      var item = new Item();
      elements.FirstOrDefault()?.MoveAttributesTo(item);
      item.Subs = elements;
      return item;
    }
  }
}
