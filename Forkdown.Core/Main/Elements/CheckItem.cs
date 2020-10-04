using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements.Types;

namespace Forkdown.Core.Elements {
  public class CheckItem : BlockContainer {
    public Element Heading => this.Subs.FirstOrDefault() ?? new Paragraph();
    public IList<Element> Content => this.Subs.SkipWhile(_ => _ == this.Heading).Where(_ => _ != this.Help).ToList();
    public Paragraph? Help => this.Subs.OfType<Paragraph>().FirstOrDefault(_ => _.Kind == ParagraphKind.Help);
    
    
  }
}
