using System.Linq;
using Forkdown.Core.Elements;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Build.Workers {
  /// <summary>
  /// Process help, info and warning paragraphs.
  /// </summary>
  public class SemanticParagraphWorker : Worker {
    public override Element ProcessElement(Element element, Arguments args) {
      if (element is Paragraph par) {
        switch (par.Subs.FirstOrDefault()) {
          case Text tw when tw.Content.StartsWith(":! "):
            par.Kind = ParagraphKind.Warning;
            tw.Content = tw.Content.Part(3);
            break;
          case Text ti when ti.Content.StartsWith(":i "):
            par.Kind = ParagraphKind.Info;
            ti.Content = ti.Content.Part(3);
            break;
          case Text th when th.Content.StartsWith(":? "):
            par.Kind = ParagraphKind.Help;
            th.Content = th.Content.Part(3);
            break;
        }
      }
      return element;
    }
  }
}
