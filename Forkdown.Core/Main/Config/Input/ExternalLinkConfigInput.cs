using System;
using System.Collections.Generic;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Config.Input {
  public class ExternalLinkConfigInput : ConfigInput<ExternalLinkConfig> {
    public virtual String DefaultUrl { get; set; } = "";

    public virtual IList<Object> Rewrites { get; set; } = Nil.L<Object>();
  }
}
