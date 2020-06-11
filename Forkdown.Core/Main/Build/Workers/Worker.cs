using Forkdown.Core.Elements;

namespace Forkdown.Core.Build.Workers {
  public interface IDocumentWorker { }
  public interface IProjectWorker {}
  
  public abstract class Worker {
    public MainBuilder? Builder = null;

    public Result<T> Process<T>(T element, Arguments args) where T : Element =>
      new Result<T>(this.ProcessElement(element, args), args);

    public abstract T ProcessElement<T>(T element, Arguments args) where T : Element;
  }
}
