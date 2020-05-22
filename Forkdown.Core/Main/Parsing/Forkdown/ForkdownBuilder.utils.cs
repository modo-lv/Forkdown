﻿using Forkdown.Core.Parsing.Forkdown.Processors;

namespace Forkdown.Core.Parsing.Forkdown {
  public partial class ForkdownBuilder {
    public static ForkdownBuilder Default =>
      new ForkdownBuilder()
        .AddProcessor<SectionProcessor>()
        .AddProcessor<GlobalIdProcessor>()
        .AddProcessor<ChecklistProcessor>();
  }
}
