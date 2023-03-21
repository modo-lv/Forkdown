using Forkdown.Core.Config;

namespace Forkdown.Core.Build; 

public class BuildContext {
  public readonly WorkerStorage Storage = new WorkerStorage();
  public MainConfig? Config;
  public LabelsConfig? LabelsConfig;
}