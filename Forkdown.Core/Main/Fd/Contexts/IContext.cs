using System;
using Forkdown.Core.Config;
using Forkdown.Core.Elements;

namespace Forkdown.Core.Fd.Contexts {
  public interface IContext {
    public MainConfig? ProjectConfig { get; set; }
    public Object? ProjectStore { get; set; }

    /// <summary>
    /// The root document that is currently being processed.
    /// </summary>
    public Document Document { get; set; }
    public Object? DocumentStore { get; set; }

    T Doc<T>() where T : new() =>
      (T) (this.DocumentStore ??= new T());

    T Proj<T>() where T : new() =>
      (T) (this.ProjectStore ??= new T());
  }
}
