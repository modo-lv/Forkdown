using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Config {
  public partial class LabelsConfig {
    private static readonly ILogger<BuildConfig> _logger = Program.Logger<BuildConfig>();

    /// <summary>
    /// Load a project's main settings file.
    /// </summary>
    /// <param name="file">File to read settings from.</param>
    /// <returns>Default project settings selectively overridden by values from the file.</returns>
    public static LabelsConfig From(FileInfo file) {
      var result = new LabelsConfig();
      if (!file.Exists) {
        _logger.LogInformation("Labels file {file} not present.", file.Name);
      }
      else
      {
        _logger.LogInformation("Loading labels from {file}...", file.Name);
        result = From(ConfigSource.From(file));
      }

      return result;
    }    
    
    
    public static LabelsConfig From(ConfigSource input) {
      var result = new LabelsConfig();

      ((IDictionary<Object, Object>) input["labels.all"]).ForEach(kv => {
        var value = (IDictionary<Object, Object>) kv.Value;
        result.All.Add(kv.Key.ToString()!, new LabelConfig(
          key: kv.Key.ToString()!,
          name: value["name"].ToString()!,
          icon: value["icon"].ToString()!
        ));
      });

      return result;
    }
  }
}
