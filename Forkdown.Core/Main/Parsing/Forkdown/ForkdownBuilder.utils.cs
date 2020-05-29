using Forkdown.Core.Parsing.Forkdown.Processors;

namespace Forkdown.Core.Parsing.Forkdown {
  public partial class ForkdownBuilder {
    public static ForkdownBuilder Default =>
      new ForkdownBuilder()
        // Label processor must always be first because it affects element's title,
        // which is used in nearly everything else.
        .AddProcessor<LabelProcessor>()
        .AddProcessor<GlobalIdProcessor>()
        .AddProcessor<ArticleProcessor>()
        .AddProcessor<ListProcessor>()
        .AddProcessor<ChecklistProcessor>();
  }
}
