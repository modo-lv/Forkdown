using System;
using System.Collections.Generic;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Config {
  public class TipsConfig {
    public IDictionary<String, TipConfig> List { get; set; } = Nil.DStr<TipConfig>();
  }
}
