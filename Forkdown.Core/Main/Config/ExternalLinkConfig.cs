using System;

namespace Forkdown.Core.Config {
  public partial class ExternalLinkConfig {

    public String UrlFor(String target) => 
      this.DefaultUrl.Replace("%~", target);

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
