using System;
using System.Collections.Generic;
using System.Linq;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Elements.Attributes {
  public class ElementSettings : Dictionary<String, String> {
    public ElementSettings() { }
    public ElementSettings(IDictionary<String, String> dictionary) : base(dictionary) { }


    /// <summary>Is the setting "on"?</summary>
    /// <remarks>
    /// A setting is considered on if it's explicitly set to <c>true</c>, or if present without an explicit value. 
    /// </remarks>
    /// <param name="setting">Name of the setting to check</param>
    /// <returns><c>true</c> if the setting is "on", <c>false</c> otherwise.</returns>
    public Boolean IsTrue(String setting) =>
      this.ContainsKey(setting) &&
      new[] { null, "", "true" }.Contains(this[setting], StringComparer.InvariantCultureIgnoreCase);

    public Boolean IsFalse(String setting) =>
      this.GetOr(setting, "").Equals("false", StringComparison.InvariantCultureIgnoreCase);

    public Boolean NotTrue(String setting) =>
      !this.IsTrue(setting);

    public Boolean NotFalse(String setting) =>
      !this.IsFalse(setting);
    
    public Boolean HasStringValue(String setting) =>
      !this.GetOr(setting, "").IsOneOf("true", "false", "");
    
  }
}
