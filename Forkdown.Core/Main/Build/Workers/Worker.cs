using Forkdown.Core.Elements;

namespace Forkdown.Core.Build.Workers {
  public interface IDocumentWorker { }
  public interface IProjectWorker {}
  
  public abstract class Worker {
    public MainBuilder? Builder = null;

    public Result Process(Element element, Arguments args) =>
      new Result(this.ProcessElement(element, args), args);

    public abstract Element ProcessElement(Element element, Arguments args);
  }
}
