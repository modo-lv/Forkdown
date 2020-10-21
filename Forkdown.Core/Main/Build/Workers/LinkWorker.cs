using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Build.Workers {
  public class LinkWorker : Worker {

    public override Element ProcessElement(Element element, Arguments args) {
      if (element is Link link) {
        if (link.Target == "@") {
          link.Target = $"@{link.TitleText}";
        }
        if (this.Builder!.Config is { } config) {
          var index = this.Builder!.Storage.GetOr(typeof(LinkIndexWorker), null) as LinkIndex;
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
