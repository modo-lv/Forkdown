using Forkdown.Core.Build.Workers;

namespace Forkdown.Core.Build {
  public partial class MainBuilder {
    public static MainBuilder CreateDefault() => new MainBuilder()
      .AddWorker<LinesToParagraphsWorker>()
      .AddWorker<ExplicitIdWorker>()
      .AddWorker<LinkIndexWorker>()
      .AddWorker<SettingsWorker>()
      .AddWorker<DocumentWorker>()
      .AddWorker<ArticleWorker>()
      .AddWorker<SemanticParagraphWorker>()
      .AddWorker<ListItemWorker>()
      .AddWorker<ImplicitIdWorker>()
      .AddWorker<CheckItemWorker>()
      .AddWorker<LinkWorker>()
      .AddWorker<SinglesIndexWorker>()
      .AddWorker<LabelWorker>();
  }
}
