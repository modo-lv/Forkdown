using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Build.Workers {
  /// <summary>
  /// Create scopes from headings for figuring out implicit IDs.
  /// </summary>
  public class IdScopeWorker : Worker {

    public override Element ProcessElement(Element element, Arguments args) {
      { // Headings can't contain other headings & list items are already a scope
        if (element is Heading || element is ListItem || element is Item)
          return element;
      }

      IdScope? scope = null;
      Heading? lastHeading = null;
      var subs = element switch {
        Item i => i.Contents,
        _ => element.Subs
      };
      var newSubs = Nil.L<Element>();
      if (element is IdScope && element.Subs.Any())
        newSubs.Add(element.Subs.First());

      subs.SkipWhile((e, i) => element is IdScope && i == 0).ForEach(el => {
        var startNewScope = el is Heading h && h.Level <= (lastHeading?.Level ?? 7);
        if (scope == null) {
          if (startNewScope)
            scope = new IdScope(el);
          else
            newSubs.Add(el);
        }
        else {
          if (startNewScope) {
            newSubs.Add(scope);
            scope = new IdScope(el);
          }
          else {
            scope.Subs.Add(el);
          }
        }

        if (startNewScope)
          lastHeading = el as Heading;

      });
      if (scope != null) {
        newSubs.Add(scope);
      }

      element.Subs = newSubs;

      return element;
    }

  }
}
