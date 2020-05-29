using System;
using Forkdown.Core.Elements.Types;

// ReSharper disable NotAccessedField.Global

namespace Forkdown.Core.Elements {
  /// <summary>
  /// A piece of forkdown content grouped in an article based on heading
  /// </summary>
  public class Article : BlockContainer {
    public Heading Heading;

    public override String Title => this.Heading.Title;

    public Article(Heading heading) {
      this.Heading = heading;
      this.Attributes = heading.Attributes;
      this.GlobalId = heading.GlobalId;
      heading.Attributes = new ElementAttributes();
      heading.GlobalId = "";
    }
  }
}
