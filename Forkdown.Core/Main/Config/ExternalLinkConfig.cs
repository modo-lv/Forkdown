using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Forkdown.Core.Config.Input;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Config {
  /// <summary>
  /// Build configuration for creating external links. 
  /// </summary>
  public class ExternalLinkConfig : ExternalLinkConfigInput {
    public new IList<KeyValuePair<Regex, String>> Rewrites { get; set; } = Nil.L<KeyValuePair<Regex, String>>();

    public ExternalLinkConfig() { }

    public ExternalLinkConfig(ExternalLinkConfigInput input) {
      this.DefaultUrl = input.DefaultUrl;
      this.Rewrites = input.Rewrites.Select(item => {
        (Object key, Object value) = item switch {
          IList<Object> list => new KeyValuePair<Object, Object>(list[0], list[1]),
          IDictionary<Object, Object> dic => new KeyValuePair<Object, Object>(dic["pattern"], dic["rewrite"]),
          _ => throw new Exception("Could not parse external link rewrite rule.")
        };
        return new KeyValuePair<Regex, String>(
          new Regex(key.ToString().Text(), RegexOptions.Compiled),
          value.ToString().Text()
        );
      }).ToList();
    }


    public String UrlFor(String target) {
      if (target.Contains("//"))
        return target;
      this.Rewrites.ForEach(_ =>
        target = _.Key.Replace(target, _.Value)
      );
      return this.DefaultUrl.Replace("%~", Uri.EscapeDataString(target));
    }

    private String _defaultUrl = "";
    /// <summary>
    /// Default URL to use when generating external links.
    /// </summary>
    public new String DefaultUrl {
      get => this._defaultUrl;
      set => this._defaultUrl = !value.Contains("%~") ? value + "%~" : value;
    }
  }
}
