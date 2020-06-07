/*
using Forkdown.Core.Parsing.Forkdown.OldProcessors;

namespace Forkdown.Core.Parsing.Forkdown {
  public partial class OldForkdownBuilder {
    public static OldForkdownBuilder Default =>
      new OldForkdownBuilder()
        // Label processor must always be first because it affects element's title,
        // which is used in nearly everything else.
        .AddProcessor<LabelProcessor>()
        .AddProcessor<GlobalIdProcessor>()
        .AddProcessor<ArticleProcessor>()
        .AddProcessor<ListProcessor>()
        .AddProcessor<ChecklistProcessor>()
        .AddProcessor<LinkProcessor>();
  }
}
*/
