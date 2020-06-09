using System.Linq;
using Forkdown.Core.Elements;

namespace Forkdown.Core.Build.Workers {
  /// <summary>
  /// A processor that handles a whole element tree at a time.
  /// </summary>
  public interface IDocumentWorker : IWorker {
    public new Result<T> Process<T>(T root, Arguments args, Context context) where T : Element =>
      new Result<T>(_proc(root, new Arguments(), context), args);

    private T _proc<T>(T el, Arguments args, Context context) where T : Element {
      var res = this.ProcessElement(el, args, context);
      res.Subs = res.Subs
        .Select(e => _proc(e, new Arguments(args), context))
        .ToList();
      return res;
    }
  }
}
