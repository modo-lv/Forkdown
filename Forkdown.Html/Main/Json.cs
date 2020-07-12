using System;
using Newtonsoft.Json;

namespace Forkdown.Html.Main {
  /// <summary>
  /// Wrapper class for JSON serialization, for use in Scriban templates.
  /// </summary>
  public static class Json {
    /// <inheritdoc cref="JsonConvert.SerializeObject(object?)"/>
    public static String Serialize(Object? obj) => JsonConvert.SerializeObject(obj);
  }
}
