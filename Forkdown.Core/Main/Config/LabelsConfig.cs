using System;
using System.Collections.Generic;
using System.Linq;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Config {
  public class LabelsConfig {
    private IDictionary<String, LabelConfig> _all = Nil.DStr<LabelConfig>();
    public IDictionary<String, LabelConfig> All {
      get => _all;
      set {
        _all = value;
        if (_all.Values.Any(v => v.Key.IsBlank())) 
          _all.ForEach(kv => kv.Value.Key = kv.Key);
      }
    }
  }
}
