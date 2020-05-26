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

      // If this is a list item, it's already a section and only needs to copy heading data, if any
      if (element is ListItem li && li.Subs[0] is Heading head) {
        li.MergeWith(head);
        newSubs = li.Subs;
      }
      else {
        IEnumerable<Element> subs = element.Subs;
        
        // If this is a section created from a heading, don't process the heading again  
        if (element is Section s && s.IsImplicit && element.Subs[0] is Heading) {
          newSubs.Add(element.Subs[0]);
          subs = element.Subs.Skip(1);
        }
      
        foreach (var el in subs.ToList()) {
          var isNextHeading = el is Heading h && h.Level <= (lastHeading?.Level ?? 7);
          if (isNextHeading)
            lastHeading = el as Heading;

          if (section == null) {
            if (isNextHeading) {
              section = new Section((Heading) el);
              section.Subs.Add(el);
            }
            else {
              newSubs.Add(el);
            }
          }
          else {
            if (isNextHeading) {
              newSubs.Add(section);
              section = new Section((Heading) el);
            }
            section.Subs.Add(el);
          }
        }
        
        if (section != null) {
          newSubs.Add(section);
        }
      }
      
      element.Subs = newSubs.Select(_ => Process(_)).ToList();

      return element;
    }
  }
}
