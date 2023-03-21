
using Forkdown.Core.Elements.Types;
using Markdig.Syntax;

namespace Forkdown.Core.Elements; 

/// <summary>
/// Arbitrary inline container, for creating inline checkboxes and other Forkdown elements.
/// </summary>
public class ExplicitInlineContainer : Element, Inline {
  public ExplicitInlineContainer(IMarkdownObject mdo) : base(mdo) { }
}