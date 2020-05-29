using System;
using System.Collections.Generic;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Config {
  public partial class ConfigSource : Dictionary<String, Object> {
   
    public ConfigSource(IDictionary<Object, Object> dictionary) {
      void addToIndex(KeyValuePair<Object, Object> kv, String parentKey = "") {
        var key = parentKey.NotBlank() ? $"{parentKey}.{kv.Key}" : kv.Key.ToString().Text();
        this[key] = kv.Value;
        if (kv.Value is IDictionary<Object, Object> dic)
          dic.ForEach(_ => addToIndex(_, key));
      }
      dictionary.ForEach(_ => addToIndex(_));
    }
  }
}
