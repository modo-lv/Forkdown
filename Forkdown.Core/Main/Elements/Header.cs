using Forkdown.Core.Elements.Types;

namespace Forkdown.Core.Elements {
  public class Header : Element, Block {
    public Header(params Element[] contents) {
      this.Subs = contents;
    }
  }
}
