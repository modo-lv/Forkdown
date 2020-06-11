using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;

namespace Forkdown.Core.Build {
  public class ElementBuilder {
    public static T Build<T>(T root, IList<Worker> workers, BuilderStorage arguments) where T : Element {
      arguments = new BuilderStorage(arguments);
      root = workers.Aggregate(root, (e, worker) => {
        var args = new Arguments(arguments.GetOrAdd(worker.GetType(), new Arguments()));
        var result = worker.Process(e, args);
        arguments[worker.GetType()] = result.Arguments;
        return result.Element;
      });
      
      root.Subs = root.Subs.Select(e => Build(e, workers, arguments)).ToList();
      return root;
    }
  }
}
