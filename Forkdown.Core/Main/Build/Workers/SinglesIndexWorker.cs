using System;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Build.Workers {
  /// <summary>
  /// Build an index of linked singleton elements.
  ///
  /// Should be run last or near the end, to make sure elements have their final IDs and titles.
  /// </summary>
  public class SinglesIndexWorker : Worker, IProjectWorker {

    public override Element ProcessElement(Element element, Arguments args) {
      var index = this.Builder!.Storage.GetOrAdd(this.GetType(), new SinglesIndex());
      var singles = args.Get<Boolean>();

      if (element is Article && element.IsCheckItem) {
        element.IsSingle = element.Settings.IsTrue("single") ||
                           singles && element.Settings.NotFalse("single");

        if (element.IsSingle) {
          if (element.ImplicitId.IsBlank())
            throw new Exception("Can't build singleton index if elements don't have their IDs set. " +
                                "Run explicit and implicit ID workers before singleton index builder.");

          index.GetOrAdd(Globals.Id(element.Title), Nil.CStr).Add(element.GlobalId);
        }

      }
      if (element.Settings.IsTrue("singles"))
        singles = true;
      else if (singles && element.Settings.IsFalse("singles"))
        singles = false;
      args.Put(singles);

      return element;
    }
  }
}
