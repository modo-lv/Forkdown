using Forkdown.Core.Build.Workers;

namespace Forkdown.Core.Build {
  public partial class MainBuilder {
    public static MainBuilder CreateDefault() => new MainBuilder()
      .AddWorker<LinesToParagraphsWorker>()
      .AddWorker<LabelWorker>()
      
      .AddWorker<HeadingItemWorker>()
      .AddWorker<DocumentWorker>()
      .AddWorker<MetaTextWorker>()
      .AddWorker<ListItemWorker>()
      .AddWorker<ItemWorker>()
      .AddWorker<ImplicitIdWorker>()
      .AddWorker<LinkWorker>()
      .AddWorker<SinglesIndexWorker>();
  }
}
