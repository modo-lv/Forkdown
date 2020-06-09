using System;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Build.Workers {
  public class LinkWorker : IElementWorker {

    public T ProcessElement<T>(T element, Arguments args, Context context) where T : Element {
      if (element is Link link) {
        if (link.Target == "@") {
          link.Target = $"@{link.Title}";
        }
        if (context.Config is { } config) {
          var index = context.BuildStore.GetOr(typeof(LinkIndexWorker), null) as LinkIndex;
          if (link.IsExternal || (!index?.ContainsKey(Globals.Id(link.Target)) ?? true)) {
            if (link.Target.StartsWith("@"))
              link.Target = link.Target.Part(1);
            link.Target = config.ExternalLinks.UrlFor(link.Target);
          }
        }
      }

      return element;
    }
  }
}
