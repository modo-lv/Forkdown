using System;
using System.Collections.Generic;
using Forkdown.Core.Elements;

namespace Forkdown.Core {
  public partial class LinkIndex {
    public static KeyValuePair<String, Document> InternalLink(KeyValuePair<String, Document> item) =>
      new KeyValuePair<String, Document>(Globals.Id(item.Key), item.Value);
  }
}
