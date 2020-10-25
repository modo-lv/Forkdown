using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Types;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Build.Workers {
  public class ImplicitIdWorker : Worker {
    public const Char G = '␝'; // Group separator
    public const Char R = '␞'; // Record separator
    public const Char W = '⸱'; // Word separator

    private readonly IDictionary<String, Int32> _times = new Dictionary<String, Int32>();

    public override Element ProcessElement(Element element, Arguments args) {
      var parentId = args.Get<String>();

      var id = parentId;

      if (element is Document dEl) {
        id = dEl.ProjectFileId;
      }
      else if (element.ExplicitId.NotBlank()) {
        id = element.ExplicitId;
      }
      else if (element is Item || element is ListItem) {
        id = element.TitleText.Trim().Replace(' ', W);

        if (parentId.NotBlank())
          id = $"{parentId}{G}{id}";
        if (id.NotBlank())
          _times[id] = _times.GetOr(id, 0) + 1;
        if (_times.GetOr(id, 0) > 1)
          id += $"{R}{_times[id]}";
        element.ImplicitId = id;
      }

      args.Put(id);
      return element;
    }
  }
}
