using System;
using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Types;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Build.Workers {
  public class LabelWorker : Worker, IProjectWorker {

    public override Element ProcessElement(Element element, Arguments args) {
      // Code labels
      if (element is Block && element.Subs.Any() && element.Subs[0] is Code labels) {
        element.Subs.RemoveAt(0);

        element.Labels = labels
          .Content
          .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
          .Select(_ => _.Trim())
          .ToHashSet();

        if (element.Subs.Any() && element.Subs[0] is Text t)
          t.Content = t.Content.TrimStart();
      }
      
      // Implicit labels
      if (element is Paragraph p && p.Subs.FirstOrDefault() is Text tx && tx.Content.StartsWith(":")) {
        if (tx.Content.StartsWith(":x ") || tx.Content.StartsWith(": ")) {
          p.Kind = ParagraphKind.Help;
          tx.Content = tx.Content.TrimPrefix(":x").TrimPrefix(":").TrimStart();
        }
        else if (tx.Content.StartsWith(":i ")) {
          p.Kind = ParagraphKind.Info;
          tx.Content = tx.Content.TrimPrefix(":i").TrimStart();
        }
        else if (tx.Content.StartsWith(":w ")) {
          p.Kind = ParagraphKind.Warning;
          tx.Content = tx.Content.TrimPrefix(":w").TrimStart();
        } 
      }
      
      return element;
    }
  }
}
