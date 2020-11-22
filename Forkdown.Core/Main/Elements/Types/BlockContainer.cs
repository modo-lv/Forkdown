using Markdig.Syntax;

namespace Forkdown.Core.Elements.Types {
  /// <summary>
  /// An element that can contain other block elements.
  /// </summary>
  public abstract class BlockContainer : Element, Block {
    protected BlockContainer() { }
    protected BlockContainer(IMarkdownObject mdo) : base(mdo) { }
  }
}
