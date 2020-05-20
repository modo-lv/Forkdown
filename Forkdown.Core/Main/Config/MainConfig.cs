using System;
using System.Collections.Generic;

namespace Forkdown.Core.Config {
  /// <summary>
  /// Main project settings.
  /// </summary>
  public partial class MainConfig {
    /// <summary>
    /// Project name. Used for display and to generate a unique ID for browser storage.
    /// </summary>
    public String Name { get; set; } = "";

    /// <summary>
    /// Settings affecting generation of external links.
    /// </summary>
    public ExternalLinkConfig ExternalLinks { get; set; } = new ExternalLinkConfig(); public class ExternalLinkConfig {
      public String UrlTo(String target) {
        return this.DefaultUrl.Replace("%~", target);
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

      public RewriteConfig Rewrites { get; set; } = new RewriteConfig(); 
      public class RewriteConfig : List<IDictionary<String, String>> {
        
      }
    }
  }
}