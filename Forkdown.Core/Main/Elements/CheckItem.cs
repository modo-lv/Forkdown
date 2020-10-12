using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements.Types;

namespace Forkdown.Core.Elements {
  public class CheckItem : BlockContainer {
    public override String Title => this.Heading?.Title ?? "";

    public Element Heading => this.Subs.FirstOrDefault() ?? new Paragraph();
    public IList<Element> Content => this.Subs.SkipWhile(_ => _ == this.Heading).ToList();
    
    public Boolean IsNewline = false;

    public CheckItem() { }

    public CheckItem(Element item) {
      this.ExplicitIds = item.ExplicitIds;
      this.Attributes = item.Attributes;
      this.Labels = item.Labels;
      this.Subs = item.Subs;
      this.ImplicitId = item.ImplicitId;
      this.IsSingle = item.IsSingle;
      this.Settings = item.Settings;
    }
  }
}
