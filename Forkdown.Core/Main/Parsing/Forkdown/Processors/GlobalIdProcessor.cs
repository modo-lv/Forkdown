using System.Linq;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  public class GlobalIdProcessor : IForkdownProcessor {
    public T Process<T>(T element, Document? doc = null) where T : Element {
      element.Attributes.Classes.ToList().Prepend(element.Attributes.Id)
        .Where(_ => _.StartsWith("#") || _ == "#~")
        .Select(_ => {
          element.Attributes.Classes.Remove(_);
          return _;
        })
        .ToList()
        .Take(1)
        .ForEach(a => element.GlobalId = GlobalId.From(a == "#~" ? element.Title : a.Part(1)));

      foreach (Element sub in element.Subs)
        Process(sub);

      return element;
    }
  }
}
