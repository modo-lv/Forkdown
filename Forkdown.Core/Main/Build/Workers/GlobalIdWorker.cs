using System;
using System.Linq;
using Forkdown.Core.Elements;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Build.Workers {
  public class GlobalIdWorker : IProjectWorker {
    public T ProcessElement<T>(T element, Arguments args, Context context) where T : Element {
      if (element.Attributes.Id.NotBlank()) {
        var ids = element.Attributes.Id.Split(',', StringSplitOptions.RemoveEmptyEntries);

        element.GlobalIds = ids
          .Select(_ => Globals.Id(_.Replace(":id", element.Title)))
          .ToList();
      }
      return element;
    }
  }
}
