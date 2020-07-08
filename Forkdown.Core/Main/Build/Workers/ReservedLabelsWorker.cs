using Forkdown.Core.Elements;

namespace Forkdown.Core.Build.Workers {
  public class ReservedLabelsWorker : Worker {

    public override Element ProcessElement(Element element, Arguments args) {
      if (element is Paragraph par) {
        if (element.Labels.Contains("w")) {
          par.Kind = ParagraphKind.Warning;
        }
        else if (element.Labels.Contains("i")) {
          par.Kind = ParagraphKind.Info;
        }
        else if (element.Labels.Contains("x")) {
          par.Kind = ParagraphKind.X;
        }
        element.Labels.Remove("i");
        element.Labels.Remove("w");
        element.Labels.Remove("x");
      }
      return element;
    }
  }
}
