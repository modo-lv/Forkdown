using System;
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

    public Document Build(String markdown, ProjectPath? file = null) =>
      this.Build(FromMarkdown.ToForkdown(markdown, file));


    public TRoot Build<TRoot>(TRoot root, MainConfig? config = null) where TRoot : Element {
      var batches = this.Chain.GroupAdjacent(p => p.GetType().GetInterfaces().Contains(typeof(ITreeProcessor)));
      var baseContext = new Context { ProjectConfig = config };
      var contexts = Nil.D<Type, IContext>();

      foreach (var batch in batches) {
        if (batch.Key) { // Tree
          root = batch.Select(_ => (ITreeProcessor) _)
            .Aggregate(root, (tree, tp) => {
                var result = tp.ProcessTree(tree, new Context(source: baseContext));
                contexts[tp.GetType()] = result.Context;
                return result.Element;
              }
            );
        }
        else { // Element
          root = process(batch, root, Nil.D<Type, Arguments>());
        }
      }

      return root;


      T process<T>(
        IEnumerable<IProcessor> procs,
        T element,
        IDictionary<Type, Arguments> args
      ) where T : Element {
        args = new Dictionary<Type, Arguments>(args);
        element = procs.Select(_ => (IElementProcessor) _)
          .Aggregate(element, (e, p) => {
            var context = contexts[p.GetType()] = new Context(contexts.GetOrAdd(p.GetType(), baseContext));
            context.Arguments = new Arguments(args.GetOrAdd(p.GetType(), new Arguments()));
            var result = p.Process(e, context);
            args[p.GetType()] = result.Context.Arguments;
            return result.Element;
          });

        element.Subs = element.Subs.Select(e => process(procs, e, args)).ToList();
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
