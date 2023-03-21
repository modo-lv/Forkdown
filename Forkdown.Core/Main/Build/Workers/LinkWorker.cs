using Forkdown.Core.Elements;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Build.Workers; 

public class LinkWorker : Worker {
  public LinkWorker() {
    this.RunsAfter<LinesToParagraphsWorker>();
    this.RunsAfter<DocumentAttributesWorker>();
    this.RunsAfter<ExplicitIdWorker>();
    this.RunsAfter<LinkIndexWorker>();
  }

  public override Element BuildElement(Element element) {
    if (element is Link link) {
      if (link.Target == "@") {
        link.Target = $"@{link.TitleText}";
      }
      if (this.Config is { } config) {
        var index = (LinkIndex)this.Storage.For<LinkIndexWorker>();
        if (link.IsExternal || !index.ContainsKey(Globals.Id(link.Target))) {
          if (link.Target.StartsWith("@"))
            link.Target = link.Target.Part(1);
          link.Target = config.ExternalLinks.UrlFor(link.Target);
        }
      }
    }

    return element;

  }
}