using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  public class LinkProcessor : IForkdownProcessor{

    public void Process<T>(T element) where T : Element {
      if (element is Link link && link.Target == "@~") {
        link.Target = $"@{link.Title}";
      }
      element.Subs.ForEach(this.Process);
    }
  }
}
