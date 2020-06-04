using System;
using System.Text.RegularExpressions;

namespace Forkdown.Core.Config {
  public partial class ExternalLinkConfig {

    public String UrlFor(String target) {
      if (target.Contains("//"))
        return target;
      this.Rewrites.ForEach(_ =>
        target = Regex.Replace(target, _.Key, _.Value)
      );
      return this.DefaultUrl.Replace("%~", Uri.EscapeDataString(target));
    }

    private String _defaultUrl = "";
    /// <summary>
    /// Default URL to use when generating external links.
    /// </summary>
    public String DefaultUrl
    {
      get => this._defaultUrl;
      set => this._defaultUrl = !value.Contains("%~") ? value + "%~" : value;
    }

    public ExternalLinkRewriteConfig Rewrites = new ExternalLinkRewriteConfig();
  }
}
