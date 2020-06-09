using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Fd.Contexts;

namespace Forkdown.Core.Fd.Processors {
  /// <summary>
  /// A processor that handles a whole element tree at a time.
  /// </summary>
  public interface ITreeProcessor : IProcessor {
    Result<TE> ProcessTree<TE>(TE tree, IContext context) where TE : Element {
      return proc(tree, context);

      // ReSharper disable once VariableHidesOuterVariable
      Result<T> proc<T>(T el, IContext parentContext) where T : Element {
        var res = this.Process(el, parentContext);
        res.Element.Subs = res.Element.Subs
          .Select(e => proc(e, new Context(source: parentContext)))
          .Select(r => r.Element)
          .ToList();
        return res;
      }
    }
  }
}
