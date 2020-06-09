using System;
using Forkdown.Core.Config;
using Forkdown.Core.Elements;

namespace Forkdown.Core.Fd.Contexts {
  public class Context : IContext {
    public virtual MainConfig? ProjectConfig { get; set; }

    public Document Document { get; set; }
    public virtual Object? DocumentStore { get; set; }
    public virtual Object? ProjectStore { get; set; }
    
    public Context(Document doc) {
      this.Document = doc;
    }

    public Context(IContext source) {
      this.Document = source.Document;
      this.ProjectConfig = source.ProjectConfig;
      this.DocumentStore = source.DocumentStore;
      this.ProjectStore = source.ProjectStore;
    }
  }
}
