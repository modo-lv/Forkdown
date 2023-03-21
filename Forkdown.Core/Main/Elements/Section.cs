using Forkdown.Core.Elements.Types;
using Markdig.Syntax;

// ReSharper disable NotAccessedField.Global

namespace Forkdown.Core.Elements; 

/// <summary>
/// An explicit section, created by using ":::" in markdown.
/// </summary>
public class Section : BlockContainer {
  public Section() { }
  public Section(IMarkdownObject mdo) : base(mdo) { }
}