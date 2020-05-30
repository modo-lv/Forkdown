using System;
using System.Collections.Generic;

namespace Forkdown.Core.Config {
  public partial class ExternalLinkConfig {
    public static ExternalLinkConfig From(ConfigSource source) {
      return new ExternalLinkConfig
      {
        DefaultUrl = source.GetValueOrDefault("externalLinks.defaultUrl") as String ?? "",
        Rewrites = ExternalLinkRewriteConfig.From(source)
      };
    }
  }
}
