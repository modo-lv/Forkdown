using System;
using System.Collections.Generic;
using System.Linq;

namespace Forkdown.Core.Elements {
  public class CheckItem : ListItem {
    public override String Title => this.Heading?.Title ?? "";

    public Element Heading => this.Subs.FirstOrDefault() ?? new Paragraph();
    public IList<Element> Content => this.Subs.SkipWhile(_ => _ == this.Heading).Where(_ => _ != this.Help).ToList();
    public Paragraph? Help => this.Subs.OfType<Paragraph>().FirstOrDefault(_ => _.Kind == ParagraphKind.Help);

    public CheckItem() { }

    public CheckItem(ListItem item) {
      this.ExplicitIds = item.ExplicitIds;
      this.Attributes = item.Attributes;
      this.Labels = item.Labels;
      this.Subs = item.Subs;
      this.ImplicitId = item.ImplicitId;
      this.IsSingle = item.IsSingle;
      this.Settings = item.Settings;
//      this.Subs.FirstOrDefault()?.MoveAttributesTo(this);
    }
  }
}
