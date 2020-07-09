using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Attributes;
using Simpler.NetCore;

namespace Forkdown.Core.Build.Workers {
  /// <summary>
  /// Mark elements that should have checkboxes.
  ///
  /// Run AFTER <see cref="ListItemWorker"/> to ensure that <see cref="ListItem.BulletChar"/> has been set. 
  /// </summary>
  public class CheckItemWorker : Worker {

    public override Element ProcessElement(Element element, Arguments args) {
      // List item
      if (element is ListItem li && li.BulletChar == '+') {
        li.Subs.FirstOrDefault().IfNotNull(e => e!.CheckItem = new CheckItemData());
      }
      // Explicit syntax
      else if (element.Settings.IsTrue("check")) {
        element.CheckItem = new CheckItemData();
      }
      // Implicit syntax
      else if (element is ExplicitInlineContainer c && c.Title.StartsWith("+ ")) {
        element.CheckItem = new CheckItemData();
      }
      
      return element;
    }
  }
}
