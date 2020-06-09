using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Config;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;
using static MoreLinq.MoreEnumerable;

namespace Forkdown.Core.Build {
  public partial class MainBuilder {
    public readonly IList<IWorker> WorkerQueue = Nil.L<IWorker>();

    public readonly BuilderStorage Storage = new BuilderStorage();

    public Document Build(String markdown, ProjectPath? file = null) =>
      this.Build(FromMarkdown.ToForkdown(markdown, file));
    
    public Document Build(Document doc, MainConfig? projectConfig = null) {
      var batches = this.WorkerQueue.GroupAdjacent(p => p.GetType().GetInterfaces().Contains(typeof(ITreeWorker)));
      var baseContext = new Context(doc, this.Storage) {
        ProjectConfig = projectConfig
      };
      
      var contexts = new ConcurrentDictionary<Type, Context>();

      foreach (var batch in batches) {
        if (batch.Key) { // Tree
          doc = batch.Select(_ => (ITreeWorker) _)
            .Aggregate(doc, (tree, tp) => {
                var context = contexts.GetOrAdd(tp.GetType(), new Context(source: baseContext));
                return tp.Process(tree, new Arguments(), context).Element;
              }
            );
        }
        else { // Element
          doc = process(batch, doc, Nil.D<Type, Arguments>());
        }
      }

      return doc;


      T process<T>(
        IEnumerable<IWorker> procs,
        T element,
        IDictionary<Type, Arguments> parentArgs
      ) where T : Element {
        parentArgs = new Dictionary<Type, Arguments>(parentArgs);
        element = procs.Select(_ => (IElementWorker) _)
          .Aggregate(element, (e, p) => {
            var context = contexts.GetOrAdd(p.GetType(), new Context(source: baseContext));
            var args = new Arguments(parentArgs.GetOrAdd(p.GetType(), new Arguments()));
            var result = p.Process(e, args, context);
            parentArgs[p.GetType()] = result.Arguments;
            return result.Element;
          });

        element.Subs = element.Subs.Select(e => process(procs, e, parentArgs)).ToList();
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
