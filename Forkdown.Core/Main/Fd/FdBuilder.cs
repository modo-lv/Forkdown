using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Config;
using Forkdown.Core.Elements;
using Forkdown.Core.Fd.Contexts;
using Forkdown.Core.Fd.Processors;
using Simpler.NetCore.Collections;
using static MoreLinq.MoreEnumerable;

namespace Forkdown.Core.Fd {
  public partial class FdBuilder {
    public readonly IList<IProcessor> Chain = Nil.L<IProcessor>();

    public readonly IDictionary<Type, Object?> ProjectStores = Nil.D<Type, Object?>();

    public Document Build(String markdown, ProjectPath? file = null) =>
      this.Build(FromMarkdown.ToForkdown(markdown, file));


    public Document Build(Document doc, MainConfig? config = null) {
      var batches = this.Chain.GroupAdjacent(p => p.GetType().GetInterfaces().Contains(typeof(ITreeProcessor)));
      var baseContext = new Context(doc) { ProjectConfig = config };
      var contexts = new ConcurrentDictionary<Type, IContext>();

      foreach (var batch in batches) {
        if (batch.Key) { // Tree
          doc = batch.Select(_ => (ITreeProcessor) _)
            .Aggregate(doc, (tree, tp) => {
                var context = contexts.GetOrAdd(tp.GetType(), new Context(source: baseContext) {
                  ProjectStore = this.ProjectStores.GetOrAdd(tp.GetType(), null)
                });
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
        IEnumerable<IProcessor> procs,
        T element,
        IDictionary<Type, Arguments> parentArgs
      ) where T : Element {
        parentArgs = new Dictionary<Type, Arguments>(parentArgs);
        element = procs.Select(_ => (IElementProcessor) _)
          .Aggregate(element, (e, p) => {
            var context = contexts.GetOrAdd(p.GetType(), new Context(source: baseContext) {
              ProjectStore = this.ProjectStores.GetOrAdd(p.GetType(), null)
            });
            var args = new Arguments(parentArgs.GetOrAdd(p.GetType(), new Arguments()));
            var result = p.Process(e, args, context);
            parentArgs[p.GetType()] = result.Arguments;
            return result.Element;
          });

        element.Subs = element.Subs.Select(e => process(procs, e, parentArgs)).ToList();
        return element;
      }
    }


    public FdBuilder AddProcessor<T>() where T : IElementProcessor, new() {
      if (this.Chain.OfType<T>().Any())
        throw new Exception($"{nameof(T)} is already registered.");
      this.Chain.Add(new T());
      return this;
    }
  }
}
