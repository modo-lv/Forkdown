using Forkdown.Core.Elements;

namespace Forkdown.Core.Build.Workers {
  public interface IWorker {
    public T ProcessElement<T>(T element, Arguments args, Context context) where T : Element;

    public Result<T> Process<T>(T element, Arguments args, Context context) where T : Element => 
      new Result<T>(this.ProcessElement(element, args, context), args);
  }
}
