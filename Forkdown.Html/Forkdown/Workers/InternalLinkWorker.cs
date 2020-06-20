using Forkdown.Core;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Simpler.NetCore.Text;

namespace Forkdown.Html.Forkdown.Workers {
  /// <summary>
  /// Change internal link keyword targets to the corresponding HTML files.
  /// </summary>
  public class InternalLinkWorker : Worker {
    private LinkIndex _index => this.Builder!.Storage.Get<LinkIndex>();

    /// <inheritdoc />
    public override T ProcessElement<T>(T element, Arguments args) {
      if (element is Link link && link.IsInternal)
      {
        var target = Globals.Id(link.Target);
        if (_index.ContainsKey(target))
          link.Target = $"{_index[target].ProjectFilePath.TrimSuffix(".md")}.html#{target}";
      }
      return element;
    }
  }
}
