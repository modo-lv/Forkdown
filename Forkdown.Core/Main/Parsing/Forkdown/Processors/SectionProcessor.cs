using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  /// <summary>
  /// Splits a forkdown element into sections based on headings.
  /// </summary>
  public class SectionProcessor : IForkdownProcessor {
    public T Process<T>(T element, Document? doc = null) where T : Element {
      Section? section = null;
      Heading? lastHeading = null;
      var newSubs = Nil.L<Element>();

      IEnumerable<Element> subs;
      if (element is Section s && s.IsImplicit && element.Subs[0] is Heading) {
        newSubs.Add(element.Subs[0]);
        subs = element.Subs.Skip(1);
      }
      else {
        subs = element.Subs;
      }

      foreach (var el in subs.ToList()) {
        var isNextHeading = el is Heading h && h.Level <= (lastHeading?.Level ?? 9);
        if (isNextHeading)
          lastHeading = el as Heading;

        if (section == null) {
          if (isNextHeading) {
            section = new Section {
              IsImplicit = true,
              Level = (el as Heading)!.Level,
            };
            section.Subs.Add(el);
            section.Attributes = el.Attributes;
            el.Attributes = new ElementAttributes();
          }
          else {
            newSubs.Add(el);
          }
        }
        else {
          if (isNextHeading) {
            newSubs.Add(section);
            section = new Section {
              IsImplicit = true,
              Level = (el as Heading)!.Level
            };
          }
          section.Subs.Add(el);
        }
      }

      if (section != null) {
        newSubs.Add(section);
      }

      element.Subs = newSubs.Select(_ => Process(_)).ToList();

      return element;
    }
  }
}
