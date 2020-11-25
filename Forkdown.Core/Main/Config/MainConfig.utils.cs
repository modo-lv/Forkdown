using System;
using System.Linq;
using System.Text;
using Fluent.IO;
using Forkdown.Core.Config.Input;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Config {
  public partial class MainConfig {
    
    public static MainConfig FromFilesIn(Path directory) {
      var input = directory.Files("*.config", false).Select(f => f.Read(Encoding.UTF8)).StringJoin(Environment.NewLine);
      return FromYaml(input);
    }

    public static MainConfig FromYaml(String input) {
      return Yaml.Deserialize<MainConfigInput>(input).ToConfig();
    }
  }
}
