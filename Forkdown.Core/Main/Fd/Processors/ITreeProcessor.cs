using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Fd.Contexts;

namespace Forkdown.Core.Fd.Processors {
  /// <summary>
  /// A processor that handles a whole element tree at a time.
  /// </summary>
  public interface ITreeProcessor : IProcessor {
    public new Result<T> Process<T>(T root, Arguments args, IContext context) where T : Element =>
      new Result<T>(_proc(root, new Arguments(), context), args);

    private T _proc<T>(T el, Arguments args, IContext context) where T : Element {
      var res = this.ProcessElement(el, args, context);
      res.Subs = res.Subs
        .Select(e => _proc(e, new Arguments(args), context))
        .ToList();
      return res;
    }
  }
}
