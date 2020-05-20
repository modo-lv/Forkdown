using System;
using System.Collections.Generic;

namespace Forkdown.Core.Config {
  public partial class ExternalLinkRewriteConfig : List<KeyValuePair<String, String>> {
    public ExternalLinkRewriteConfig() { }
    public ExternalLinkRewriteConfig(IEnumerable<KeyValuePair<String, String>> collection) : base(collection) { }
  }
}
