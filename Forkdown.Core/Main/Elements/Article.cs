using System;
using Forkdown.Core.Elements.Types;
using Simpler.NetCore.Collections;

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
      this.GlobalIds = heading.GlobalIds;
      heading.Attributes = new ElementAttributes();
      heading.GlobalIds = Nil.LStr;
    }
  }
}
