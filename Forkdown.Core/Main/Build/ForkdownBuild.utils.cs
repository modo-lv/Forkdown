using Forkdown.Core.Build.Workers;

namespace Forkdown.Core.Build {
  public partial class ForkdownBuild {
    public static ForkdownBuild Default => new ForkdownBuild()
      .AddWorker<LinesToParagraphsWorker>()
      .AddWorker<DocumentAttributesWorker>()
      .AddWorker<LinkIndexWorker>()
      .AddWorker<LinkWorker>()
      .AddWorker<HeadingItemWorker>()
      .AddWorker<ImplicitIdWorker>()
      .AddWorker<ItemWorker>()
      .AddWorker<TipWorker>()
      .AddWorker<ListItemWorker>()
      .AddWorker<MetaTextWorker>()
      .AddWorker<SinglesIndexWorker>();
  }
}
