using System;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Elements {
  /// <summary>
  /// Reusable element meant to be displayed in the form of a small image/character with a tooltip
  /// (or in some other space-saving way).
  /// </summary>
  public class Tip : Element {
    /// <summary>
    /// Element to use as the button/icon for the tip.
    /// One to few characters of text (e.g., letters, emoji) or small image recommended.
    /// </summary>
    public Element Icon {
      get => _icon ?? new Text(this.ExplicitId);
      set => _icon = value;
    }
    private Element? _icon;

    /// <summary>
    /// Item title to display as the default tooltip / alt-text for images.
    /// </summary>
    private String? _title;
    public String Title {
      get => _title.NonBlank() ?? this.ExplicitId;
      set => _title = value;
    }

    /// <inheritdoc cref="Tip"/>
    public Tip(String id) {
      this.ExplicitIds.Add(id);
    }
  }
}
