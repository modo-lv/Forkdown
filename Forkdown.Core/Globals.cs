using System;
using System.Text.RegularExpressions;
using Forkdown.Core.Elements;
using Simpler.NetCore.Text;

namespace Forkdown.Core {
  public class Globals {
    /// <summary>
    /// Prefix to when Forkdown-specific element attributes share a space with user-provided ones
    /// </summary>
    public const String Prefix = "fd--";

    /// <summary>
    /// Convert a string to a valid global ID.
    /// </summary>
    /// <param name="input">String to convert.</param>
    /// <returns>A syntactically valid global ID.</returns>
    public static String Id(String input) =>
      Regex.Replace(input.Text().Trim().Replace(" ", "_"), @"[^\w_-]", "").ToLowerInvariant();
  }
}
