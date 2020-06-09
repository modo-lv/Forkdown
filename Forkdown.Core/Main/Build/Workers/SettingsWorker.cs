using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Attributes;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Build.Workers {
  /// <summary>
  /// Parses and applies Forkdown's <c>{:setting=value}</c> settings from element HTML attributes.
  /// </summary>
  public class SettingsWorker : IDocumentWorker, IElementWorker {

    public T ProcessElement<T>(T element, Arguments args, Context context) where T : Element {
      var html = element.Attributes;
      if (html.Properties != null) {
        // ReSharper disable once RedundantCast
        element.Settings = new ElementSettings(
          html.Properties
            .Where(_ => _.Key?.StartsWith(":") ?? false)
            .ToDictionary(_ => _.Key.Part(1), _ => _.Value)
        );

        html.Properties.RemoveAll(_ => _.Key.StartsWith(":"));
      }

      return element;
    }
  }
}
