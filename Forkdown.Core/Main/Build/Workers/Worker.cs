using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Config;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Build.Workers {
  public abstract class Worker {

    public BuildContext? Context;

   
    public readonly ISet<Type> MustRunAfter = Nil.S<Type>();


    protected MainConfig? Config => this.Context!.Config;
    protected WorkerStorage Storage => this.Context!.Storage;
    public T Stored<T>(T initValue) => (T)this.Storage.For(this, initValue);



    public virtual Worker RunsAfter<T>() where T : Worker {
      this.MustRunAfter.Add(typeof(T));
      return this;
    }



    public virtual Element BuildElement(Element element) =>
      throw new NotImplementedException();


    public virtual Element BuildTree(Element root) {
      var result = this.BuildElement(root);
      result.Subs = result.Subs.Select(this.BuildTree).ToList();
      return result;
    }

  }

}
