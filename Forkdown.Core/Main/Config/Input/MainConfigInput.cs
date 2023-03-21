using System;

namespace Forkdown.Core.Config.Input; 

public class MainConfigInput : ConfigInput<MainConfig> {
  public virtual String Name { get; set; } = "";

  public virtual ExternalLinkConfigInput ExternalLinks { get; set; } = new ExternalLinkConfigInput();

  public virtual LabelsConfig Labels { get; set; } = new LabelsConfig();

  public virtual TipsConfig Tips { get; set; } = new TipsConfig();
    
}