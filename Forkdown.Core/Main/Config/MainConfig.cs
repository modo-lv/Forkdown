using System;

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
    public ExternalLinkConfig ExternalLinks { get; set; } = new ExternalLinkConfig();
  }
}