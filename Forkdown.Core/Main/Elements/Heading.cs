using System;
using Forkdown.Core.Main.Elements;
using Markdig.Syntax;

namespace Forkdown.Core.Elements {
  public class Heading : Container {
    public Int32 Level;

    public Heading(HeadingBlock mHeading) : base(mHeading) {
      this.Level = mHeading.Level;
    }
  }
}