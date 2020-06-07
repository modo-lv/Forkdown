using Forkdown.Core.Parsing.Forkdown.Processors;

namespace Forkdown.Core.Parsing.Forkdown {
  public partial class ForkdownBuilder {
    public static ForkdownBuilder Default = new ForkdownBuilder()
      // Document
      .AddProcessor<LabelProcessor>()
      .AddProcessor<SettingsProcessor>()
      .AddProcessor<GlobalIdProcessor>()
      .AddProcessor<DocumentProcessor>()
      // Element
      .AddProcessor<ArticleProcessor>()
      .AddProcessor<ListItemProcessor>()
      .AddProcessor<ChecklistProcessor>()
      .AddProcessor<CheckboxIdProcessor>()
      .AddProcessor<LinkProcessor>();
  }
}
