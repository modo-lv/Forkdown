using Forkdown.Core.Build.Workers;

namespace Forkdown.Core.Build {
  public partial class MainBuilder {
    public static MainBuilder CreateDefault() => new MainBuilder()
      .AddWorker<LabelWorker>()
      .AddWorker<GlobalIdWorker>()
      .AddWorker<LinkIndexWorker>()
      /*

      .AddWorker<SettingsWorker>()
      .AddWorker<DocumentWorker>()

      .AddWorker<ArticleWorker>()
      .AddWorker<ListItemWorker>()
      .AddWorker<ChecklistWorker>()
      .AddWorker<CheckboxIdWorker>()
      */
      .AddWorker<LinkWorker>();
  }
}
