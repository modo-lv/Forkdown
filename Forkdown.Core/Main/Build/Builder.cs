using System;
using System.Collections.Generic;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;

namespace Forkdown.Core.Build {
  public class Builder {

    private readonly Worker _worker;

    public ISet<Type> MustRunAfter => this._worker.MustRunAfter;
    public readonly Type Type;

    public Builder(Type workerType, BuildContext context) {
      this.Type = workerType;
      _worker = (Worker) Activator.CreateInstance(this.Type)!;
      _worker.Context = context;
    }

    public T Build<T>(T input) where T : Element =>
      _worker.BuildTree(input);


    public override Boolean Equals(Object? obj) => obj is Builder b && this.Type == b.Type;
    public override Int32 GetHashCode() => this.Type.GetHashCode();
  }
}
