using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Forkdown.Html.Main {
  /// <summary>
  /// Configuration for controlling HTML, CSS and JS buildings.
  /// </summary>
  [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
  public partial class HtmlConfig {
    /// <summary>
    /// Minimum width that any article element is allowed to be.
    /// </summary>
    /// <remarks>
    /// Must be a value that isn't relative to parent element size.
    /// em, rem, px and similar should work fine, maybe even vh. Percentages won't work, they will create permanent
    /// columns causing horizontal scrolling on small screens.
    /// </remarks>
    public String MinArticleWidth = "350px";

    //public String GridAutoRows = "1rem";
  }
}
