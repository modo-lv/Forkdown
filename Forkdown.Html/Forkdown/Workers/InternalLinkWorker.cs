using Forkdown.Core;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Simpler.NetCore.Text;

namespace Forkdown.Html.Forkdown.Workers {
  /// <summary>
  /// Sets the <see cref="Link.Target"/> field of internal links to the relative URL in HTML output.  
  /// </summary>
  public class InternalLinkWorker : Worker {
    /// <inheritdoc />
    public override Element BuildElement(Element element) {
      var index = (LinkIndex)this.Storage.For<LinkIndexWorker>();
      if (element is Link link && link.IsInternal)
      {
        var target = Globals.Id(link.Target);
        if (index.ContainsKey(target))
          link.Target = $"{index[target].ProjectFilePath.TrimSuffix(".md")}.html#{target}";
      }
      return element;

    }
  }
}
