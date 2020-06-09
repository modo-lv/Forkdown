using Forkdown.Core.Build.Workers;

namespace Forkdown.Core.Build {
  public partial class MainBuilder {
    public static MainBuilder CreateDefault() => new MainBuilder()
      .AddWorker<LabelWorker>()
      .AddWorker<SettingsWorker>()
      .AddWorker<GlobalIdWorker>()
      .AddWorker<DocumentWorker>()
      
      .AddWorker<ArticleWorker>()
      .AddWorker<ListItemWorker>()
      .AddWorker<ChecklistWorker>()
      .AddWorker<CheckboxIdWorker>()
      .AddWorker<LinkIndexWorker>()
      .AddWorker<LinkWorker>();
  }
}
