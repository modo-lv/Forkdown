using Forkdown.Core.Build.Workers;

namespace Forkdown.Core.Build {
  public partial class MainBuilder {
    public static MainBuilder CreateDefault() => new MainBuilder()
      .AddWorker<LinesToParagraphsWorker>()
      .AddWorker<ExplicitIdWorker>()
      .AddWorker<LinkIndexWorker>()
      .AddWorker<SettingsWorker>()
      .AddWorker<HeadingItemWorker>()
      .AddWorker<DocumentWorker>()
      .AddWorker<SemanticParagraphWorker>()
      .AddWorker<ListItemWorker>()
      .AddWorker<ItemWorker>()
      .AddWorker<ImplicitIdWorker>()
      .AddWorker<LinkWorker>()
      .AddWorker<SinglesIndexWorker>()
      .AddWorker<LabelWorker>();
  }
}
