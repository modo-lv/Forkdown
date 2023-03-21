using System;
using System.Collections.Generic;
using Forkdown.Core.Build.Workers;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Build; 

public class WorkerStorage : Dictionary<Type, Object?> {
  public Object For(Worker worker, Object? init = null) => this.For(worker.GetType(), init);
  public Object For<TBuilder>(Object? init = null) where TBuilder : Worker => this.For(typeof(TBuilder), init);
  public Object For(Type builderType, Object? init = null) => this.GetOrAdd(builderType, init)!;
}