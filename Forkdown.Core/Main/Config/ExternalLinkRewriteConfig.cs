using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Forkdown.Core.Config {
  public partial class ExternalLinkRewriteConfig : List<KeyValuePair<Regex, String>> {
    public ExternalLinkRewriteConfig() { }
    public ExternalLinkRewriteConfig(IEnumerable<KeyValuePair<Regex, String>> collection) : base(collection) { }
  }
}
