using System;
using Forkdown.Core.Elements;

namespace Forkdown.Core {
  public class Globals {
    /// <summary>
    /// Prefix to when Forkdown-specific element attributes share a space with user-provided ones
    /// </summary>
    public const String Prefix = "fd--";

    /// <summary>
    /// Key in <see cref="Element.Data"/> where HTML attributes are stored. 
    /// </summary>
    public const String HtmlDataKey = "HtmlAttributes";
  }
}
