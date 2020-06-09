using System;
using Forkdown.Core.Config;

namespace Forkdown.Core.Fd.Contexts {
  public class Context : IContext {
    public virtual MainConfig? ProjectConfig { get; set; }

    public virtual Arguments Arguments { get; set; } = new Arguments();
    public virtual Object? DocumentStore { get; set; }
    public virtual Object? ProjectStore { get; set; }

    public Context() { }

    public Context(IContext source) {
      this.ProjectConfig = source.ProjectConfig;
      this.Arguments = new Arguments(source.Arguments);
      this.DocumentStore = source.DocumentStore;
      this.ProjectStore = source.ProjectStore;
    }
  }
}
