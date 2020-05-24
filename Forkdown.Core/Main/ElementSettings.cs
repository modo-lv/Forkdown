using System;
using System.Collections.Generic;
using Simpler.Net;
using Simpler.NetCore.Text;

namespace Forkdown.Core {
  public class ElementSettings : Dictionary<String, String?> {
    public Boolean IsTrue(String setting) =>
      this.ContainsKey(setting) && !this[setting].Text().Equals("false", StringComparison.InvariantCultureIgnoreCase);

    public Boolean IsFalse(String setting) =>
      this.Get(setting).Text().Equals("false", StringComparison.InvariantCultureIgnoreCase);

    public Boolean NotTrue(String setting) =>
      !this.IsTrue(setting);

    public Boolean NotFalse(String setting) => 
      !this.IsFalse(setting);
  }
}
