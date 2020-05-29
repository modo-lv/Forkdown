using System;
using System.Collections.Generic;
using System.Linq;
using Simpler.NetCore.Text;
using Yaml = YamlDotNet.Serialization;

namespace Forkdown.Core.Config {
  public partial class ExternalLinkRewriteConfig {

    public static ExternalLinkRewriteConfig From(String yaml) =>
      From(ConfigSource.From(yaml));

    public static ExternalLinkRewriteConfig From(IDictionary<String, Object> input) {
      var items = ((IList<Object>) input["external_links.rewrites"]).Select(item => {
        (Object key, Object value) = item switch
        {
          IList<Object> list => new KeyValuePair<Object, Object>(list[0], list[1]),
          IDictionary<Object, Object> dic => new KeyValuePair<Object, Object>(dic["pattern"], dic["rewrite"]),
          _ => throw new Exception("Could not parse external link rewrite rule.")
        };
        return new KeyValuePair<String, String>(key.ToString().Text(), value.ToString().Text());
      });

      return new ExternalLinkRewriteConfig(items);
    }
  }
}
