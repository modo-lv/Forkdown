/*
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Parsing.Forkdown.OldProcessors {
  public class LinkProcessor : IForkdownProcessor{

    public void Process<T>(T element) where T : Element {
      if (element is Link link && link.Target.IsOneOf("@~", "~")) {
        link.Target = link.Target.Replace("~", link.Title);
      }
      element.Subs.ForEach(this.Process);
    }
  }
}
*/
