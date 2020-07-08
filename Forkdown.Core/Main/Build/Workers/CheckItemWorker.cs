using Forkdown.Core.Elements;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Build.Workers {
  /// <summary>
  /// Convert (wrap) marked elements into checkitems.
  /// </summary>
  public class CheckItemWorker : Worker {

    public override Element ProcessElement(Element element, Arguments args) {
      if (element is ExplicitInlineContainer c) {
        if (c.Settings.IsTrue("check"))
          return new CheckItem { Subs = c.Subs };
        var text = c.FindSub<Text>();
        if (text?.Content.StartsWith("+ ") ?? false) {
          text!.Content = text!.Content.TrimPrefix("+ "); 
          return new CheckItem { Subs = c.Subs };
        }
      }

      return element;
    }
  }
}
