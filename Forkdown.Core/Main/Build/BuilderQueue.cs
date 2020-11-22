using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Build {
  public class BuilderQueue : IEnumerable<Builder> {

    private readonly IList<Builder> _queue = Nil.L<Builder>();

    public BuilderQueue(IEnumerable<Type> workers, BuildContext context) {
      var builders = workers.Select(w => new Builder(w, context));

      this.QueueBuilders(builders, context, Nil.D<Type, ICollection<Type>>());
    }

    protected void QueueBuilders(
      IEnumerable<Builder> builders,
      BuildContext context,
      IDictionary<Type, ICollection<Type>> dependencies
    ) {
      var bs = builders.ToList();
      bs.Except(_queue).ForEach(builder => {
        var runBefore = builder.MustRunAfter.Select(d => {
          if (dependencies.GetOr(d, Nil.S<Type>()).Contains(builder.Type))
            throw new Exception($"Circular dependency: {d} and {builder.Type} depend on each other.");
          return bs.SingleOrDefault(_ => _.Type == d) ?? new Builder(d, context);
        });
        this.QueueBuilders(runBefore, context, dependencies);

        dependencies.GetOrAdd(builder.Type, Nil.S<Type>());
        if (!_queue.Contains(builder))
          _queue.Add(builder);
      });
    }

    public IEnumerator<Builder> GetEnumerator() => this._queue.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
  }
}
