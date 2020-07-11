using Forkdown.Core.Elements;

namespace Forkdown.Core.Build.Workers {
  public interface IDocumentWorker { }
  public interface IProjectWorker {}
  
  public abstract class Worker {
    public MainBuilder? Builder = null;

    public Result Process(Element element, Arguments args) =>
      new Result(this.ProcessElement(element, args), args);

    /// <summary>
    /// Process an element.
    ///
    /// Will be called on every element in a tree, starting from the root.
    /// </summary>
    /// <param name="element">Element to process</param>
    /// <param name="args">Arguments passed from parent element to sub-elements.</param>
    /// <returns></returns>
    public abstract Element ProcessElement(Element element, Arguments args);
  }
}
