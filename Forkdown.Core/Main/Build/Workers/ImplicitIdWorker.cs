using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Build.Workers {
  public class ImplicitIdWorker : Worker {
    public const Char G = '␝'; // Group separator
    public const Char R = '␞'; // Record separator
    public const Char W = '⸱'; // Word separator

    private IDictionary<String, Int32> _times = new Dictionary<String, Int32>();

    public ImplicitIdWorker() {
      this.RunsAfter<LinesToParagraphsWorker>();
      this.RunsAfter<DocumentAttributesWorker>();
      this.RunsAfter<ExplicitIdWorker>();
      this.RunsAfter<HeadingItemWorker>();
      this.RunsAfter<ListItemWorker>();
    }

    public override TElement BuildTree<TElement>(TElement root) {
      this._times = Nil.DStr<Int32>();
      return (TElement) this.BuildElement(root, root.GlobalId);
    }

    protected Element BuildElement(Element element, String parentId = "") {
      String id = parentId;

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

      element.Subs = element.Subs.Select(el => this.BuildElement(el, id)).ToList();

      return element;
    }
  }
}
