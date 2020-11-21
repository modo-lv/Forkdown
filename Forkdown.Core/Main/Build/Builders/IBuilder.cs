using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements;
using Simpler.NetCore;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Build.Builders {
  public abstract class Builder {

    public readonly ISet<Type> MustHaveRun = Nil.S<Type>();

    protected Builder MustRunAfter<TBuilder>() where TBuilder : Builder {
      this.MustHaveRun.Add(typeof(TBuilder));
      return this;
    }
    
    /// <summary>
    /// Build logic for a single element. Override this to focus on one atomic element at a time.
    /// </summary>
    protected virtual TElement BuildElement<TElement>(TElement element, Maybe<Object> results) where TElement : Element {
      throw new NotImplementedException();
    }
    
    /// <summary>
    /// Build logic for the whole element tree. Override this to control the tree travel. 
    /// </summary>
    public virtual TElement BuildTree<TElement>(TElement root, Maybe<Object> results) where TElement : Element {
      var result = this.BuildElement(root, results);
      result.Subs = root.Subs.Select(_ => this.BuildTree(_, results)).ToList();
      return result;
    }
  }
}
