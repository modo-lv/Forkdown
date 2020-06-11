using System;
using System.Linq;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Build.Workers {
  public class GlobalIdWorker : Worker, IProjectWorker {

    public override T ProcessElement<T>(T element, Arguments args) {
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
