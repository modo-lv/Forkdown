using System;
using System.Linq;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Build.Workers {
  public class SinglesIndexWorker : Worker {
    private SinglesIndex? _index;
    
    public SinglesIndexWorker() {
      this.RunsAfter<ListItemWorker>();
      this.RunsAfter<ItemWorker>();
      this.RunsAfter<ExplicitIdWorker>();
      this.RunsAfter<ImplicitIdWorker>();
    }

    public override TElement BuildTree<TElement>(TElement root) {
      _index = this.Stored(new SinglesIndex());
      return (TElement) _build(root);
    }

    private Element _build(Element el, Boolean singles = false) {
      if (el is Item check && check.IsCheckitem) {
        el.IsSingle = el.Settings.NotFalse("single") && (
          singles || el.Settings.ContainsKey("single")
        );

        if (el.IsSingle) {
          if (el.GlobalId.IsBlank())
            throw new Exception("Can't build singleton index if elements don't have their IDs set. " +
                                "Run explicit and implicit ID workers before singleton index builder.");

          var singleIds = el.Settings.HasStringValue("single")
            ? el.Settings["single"].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(_ => _.Trim())
            : new[] { Globals.Id(check.TitleText) };

          singleIds.ForEach(_ => _index!.GetOrAdd(_, Nil.CStr).Add(el.GlobalId));
        }

      }
      if (el.Settings.IsTrue("singles"))
        singles = true;
      else if (singles && el.Settings.IsFalse("singles"))
        singles = false;

      el.Subs = el.Subs.Select(e => _build(e, singles)).ToList();

      return el;
    }
  }

}
