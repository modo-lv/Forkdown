using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements.Types;

// ReSharper disable NotAccessedField.Global

namespace Forkdown.Core.Elements {
  /// <summary>
  /// A piece of forkdown content grouped in an article based on a heading
  /// </summary>
  public class Article : BlockContainer {
    public Header Header => (Header) this.Subs.FirstOrDefault();

    public IList<Element> Contents => this.Subs.Skip(1).ToList();

    // ReSharper disable once SuggestBaseTypeForParameter
    public Article(Element? heading) {
      this.Subs.Insert(0, new Header(heading));
      heading?.MoveAttributesTo(this);
    }
  }
}
