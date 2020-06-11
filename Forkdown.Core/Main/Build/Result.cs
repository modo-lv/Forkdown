using Forkdown.Core.Elements;

namespace Forkdown.Core.Build {
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
