using Forkdown.Core.Elements;
using Forkdown.Core.Fd.Contexts;

namespace Forkdown.Core.Fd.Processors {
  public interface IProcessor {
    public Result<T> Process<T>(T element, IContext context) where T : Element;
  }
}
