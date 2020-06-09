using System;
using Forkdown.Core.Elements;
using Forkdown.Core.Fd.Contexts;

namespace Forkdown.Core.Fd {
  public class Result<TElement> where TElement : Element
  {
    public readonly TElement Element;
    public readonly Arguments Arguments;
    
    public Result(TElement element, Arguments arguments) {
      this.Element = element;
      this.Arguments = arguments;
    }
  }
}
