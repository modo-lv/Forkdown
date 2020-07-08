using Forkdown.Core.Elements;

namespace Forkdown.Core.Build {
  public class Result
  {
    public readonly Element Element;
    public readonly Arguments Arguments;
    
    public Result(Element element, Arguments arguments) {
      this.Element = element;
      this.Arguments = arguments;
    }
  }
}
