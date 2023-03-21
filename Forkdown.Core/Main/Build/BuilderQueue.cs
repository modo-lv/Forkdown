using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Build; 

public class BuilderQueue : IEnumerable<Builder> {

  private readonly IList<Builder> _queue = Nil.L<Builder>();

  public BuilderQueue(IEnumerable<Type> workers, BuildContext context) {
    var builders = workers.Select(w => new Builder(w, context));

    this.QueueBuilders(builders, context, Nil.S<Type>());
  }

  protected void QueueBuilders(
    IEnumerable<Builder> builders,
    BuildContext context,
    ISet<Type> ancestors
  ) {
    var bs = builders.ToList();
    bs.Except(_queue).ForEach(builder => {
      var runBefore = builder.MustRunAfter.Select(d => {
        if (ancestors.Contains(d))
          throw new Exception($"Circular dependency between {d.Name} and {builder.Type.Name}.");
        return bs.SingleOrDefault(_ => _.Type == d) ?? new Builder(d, context);
      }).ToHashSet();
      this.QueueBuilders(runBefore, context, ancestors.Append(builder.Type).ToHashSet());

      if (!_queue.Contains(builder))
        _queue.Add(builder);
    });
  }

  public IEnumerator<Builder> GetEnumerator() => this._queue.GetEnumerator();
  IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}