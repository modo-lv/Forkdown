using Forkdown.Core.Fd.Processors;

namespace Forkdown.Core.Fd {
  public partial class FdBuilder {
    public static FdBuilder Default = new FdBuilder()
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
