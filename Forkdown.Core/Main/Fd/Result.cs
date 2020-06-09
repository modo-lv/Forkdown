using System;
using Forkdown.Core.Elements;
using Forkdown.Core.Fd.Contexts;

namespace Forkdown.Core.Fd {
  public class Result<TElement> where TElement : Element
  {
    public readonly TElement Element;
    public readonly IContext Context;
    
    public Result(TElement element, IContext context) {
      this.Element = element;
      this.Context = context;
    }
  }
}
