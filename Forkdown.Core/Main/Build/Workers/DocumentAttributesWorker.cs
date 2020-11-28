using System.Linq;
using Forkdown.Core.Elements;

namespace Forkdown.Core.Build.Workers {
  public class DocumentAttributesWorker : Worker {
    public DocumentAttributesWorker() {
      this.RunsAfter<ExplicitIdWorker>();
    }

    public override Element BuildTree(Element root) {
      if (!(root is Document))
        return root;
      if (root.Subs.FirstOrDefault() is Paragraph par &&
          par.Subs.FirstOrDefault() is Text text &&
          text.Content == ":") {
        par.MoveAttributesTo(root);
      }
      return root;
    }
  }
}
