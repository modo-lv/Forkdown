using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Config;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;
using static MoreLinq.Extensions.GroupAdjacentExtension;

namespace Forkdown.Core.Build {
  public partial class MainBuilder {
    public readonly IList<IWorker> WorkerQueue = Nil.L<IWorker>();

    public readonly BuilderStorage Storage = new BuilderStorage();

    public Document Build(Document doc, BuildConfig? config = null) {
      return this.Build(new[] { doc }, config).Single();
    }

    public Document Build(String markdown, BuildConfig? config = null, ProjectPath? file = null) =>
      this.Build(FromMarkdown.ToForkdown(markdown, file), config);

    public IEnumerable<Document> Build(IEnumerable<Document> docs, BuildConfig? config = null) {
      var contexts = new Dictionary<Type, Context>();

      docs = this.WorkerQueue.OfType<IProjectWorker>().Aggregate(docs, (ds, worker) => ds.Select(doc => {
        var context = contexts.GetOrAdd(worker.GetType(), new Context(doc, this.Storage) { Config = config });
        return worker.Process(doc, new Arguments(), context).Element;
      })).ToList();

      return docs.Select(doc => {
        var batches = this.WorkerQueue
          .Where(_ => !(_ is IProjectWorker))
          .GroupAdjacent(worker => worker is IDocumentWorker);
        var baseContext = new Context(doc, this.Storage) {
          Config = config
        };

        foreach (var batch in batches) {
          if (batch.Key) { // Tree
            doc = batch.Select(_ => (IDocumentWorker) _)
              .Aggregate(doc, (tree, worker) => {
                  var c = contexts.GetOrAdd(worker.GetType(), new Context(source: baseContext));
                  return worker.Process(tree, new Arguments(), c).Element;
                }
              );
          }
          else { // Element
            doc = process(batch, doc, Nil.D<Type, Arguments>(), baseContext);
          }
        }

        return doc;
      });


      T process<T>(
        IEnumerable<IWorker> procs,
        T element,
        IDictionary<Type, Arguments> parentArgs,
        Context baseContext
      ) where T : Element {
        parentArgs = new Dictionary<Type, Arguments>(parentArgs);
        element = procs.Select(_ => (IElementWorker) _)
          .Aggregate(element, (e, p) => {
            var context = contexts.GetOrAdd(p.GetType(), new Context(source: baseContext));
            var args = new Arguments(parentArgs.GetOrAdd(p.GetType(), new Arguments()));
            var result = p.Process(e, args, baseContext);
            parentArgs[p.GetType()] = result.Arguments;
            return result.Element;
          });

        element.Subs = element.Subs.Select(e => process(procs, e, parentArgs, baseContext)).ToList();
        return element;
      }
    }


    public MainBuilder AddWorker<T>() where T : IWorker, new() {
      if (this.WorkerQueue.OfType<T>().Any())
        throw new Exception($"{nameof(T)} is already registered.");
      this.WorkerQueue.Add(new T());
      return this;
    }
  }
}
