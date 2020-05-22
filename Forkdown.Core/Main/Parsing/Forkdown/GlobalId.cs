using System;
using System.Text.RegularExpressions;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Parsing.Forkdown {
  public class GlobalId {
    public static String From(String input) =>
      Regex.Replace(input.Text().Trim().Replace(" ", "_"), @"[^\w_-]", "").ToLowerInvariant();
  }
}
