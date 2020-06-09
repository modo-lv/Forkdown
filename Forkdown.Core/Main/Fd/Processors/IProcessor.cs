using Forkdown.Core.Elements;
using Forkdown.Core.Fd.Contexts;

namespace Forkdown.Core.Fd.Processors {
  public interface IProcessor {
    public T ProcessElement<T>(T element, Arguments args, IContext context) where T : Element;

    public Result<T> Process<T>(T element, Arguments args, IContext context) where T : Element => 
      new Result<T>(this.ProcessElement(element, args, context), args);
  }
}
