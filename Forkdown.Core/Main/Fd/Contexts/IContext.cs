using System;
using Forkdown.Core.Config;
using Forkdown.Core.Elements;

namespace Forkdown.Core.Fd.Contexts {
  public interface IContext {
    public MainConfig? ProjectConfig { get; set; }

    public Arguments Arguments { get; set; }
    public Object? DocumentStore { get; set; }
    public Object? ProjectStore { get; set; }


    String GetArg() => this.Arguments.Get();
    T GetArg<T>(T fallback) => this.Arguments.GetOr(fallback);
    T GetArg<T>() where T : unmanaged => this.Arguments.Get<T>();

    void SetArg<T>(T arg) => this.Arguments[""] = arg;

    T Doc<T>() where T : new() =>
      (T) (this.DocumentStore ??= new T());

    Result<T> ToResult<T>(T element) where T : Element =>
      new Result<T>(element, this);
  }
}
