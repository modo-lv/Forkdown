using System.Linq;
using Forkdown.Core.Elements.Types;

namespace Forkdown.Core.Elements {
  public class IdScope : BlockContainer {
    public IdScope(Element heading) {
      this.ExplicitIds = heading.ExplicitIds;
      this.Subs.Add(heading);
    }

    public override void MoveAttributesTo(Element element) {
      this.Subs.First().MoveAttributesTo(element);
    }
  }
}
