using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Attributes;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Build.Workers {
  /// <summary>
  /// Parses and applies Forkdown's <c>{:setting=value}</c> settings from element HTML attributes.
  /// </summary>
  public class SettingsWorker : Worker, IDocumentWorker {

    public override Element ProcessElement(Element element, Arguments args) {
      var html = element.Attributes;
      if (html.Properties != null) {
        var settings = html.Properties.Where(_ => _.Key?.StartsWith(":") ?? false).ToHashSet();
        settings.ForEach(_ => {
          element.Settings[_.Key.Part(1)] = _.Value?.Trim() ?? "";
          html.Properties.Remove(_);
        });
      }

      return element;
    }
  }
}
