using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Build.Workers;
using CollectionExtensions = Simpler.NetCore.Collections.CollectionExtensions;

namespace Forkdown.Core.Build {
  public class BuilderStorage : Dictionary<Type, Object?> {

    public BuilderStorage() { }
    public BuilderStorage(IDictionary<Type, Object?> source) : base(source) { }


    public TStore Get<TWorker, TStore>() where TWorker : Worker =>
      (TStore) this[typeof(TWorker)]!;

    public TStore Get<TStore>() =>
      this.Values.OfType<TStore>().Single();

    public TStore GetOrAdd<TStore>(Type key, TStore value) =>
      (TStore) CollectionExtensions.GetOrAdd(this, key, value)!;
  }
}
