using System;
using Markdig.Syntax;
using Block = Forkdown.Core.Elements.Types.Block;

namespace Forkdown.Core.Elements.Markdown; 

public class Heading : Element, Block {
  public readonly Int32 Level;

  public Heading(Int32 level) { this.Level = level; }

  public Heading(HeadingBlock mHeading) : base(mHeading) {
    this.Level = mHeading.Level;
  }
}