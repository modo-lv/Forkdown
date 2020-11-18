using System;
using System.Collections.Generic;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Config {
  public partial class LabelsConfig {
    public readonly IDictionary<String, LabelConfig> All = Nil.DStr<LabelConfig>();
  }
}
