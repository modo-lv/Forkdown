using System;
using Markdig.Syntax.Inlines;
using Inline = Forkdown.Core.Elements.Types.Inline;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Forkdown.Core.Elements; 

/// <summary>
/// A link to another page or an external resource.
/// </summary>
public class Link : Element, Inline {
  /// <summary>
  /// Is this a link to an external resource?
  /// </summary>
  public Boolean IsExternal { get; protected set; }
  /// <summary>
  /// Is this a link to another document in the project?
  /// </summary>
  public Boolean IsInternal => !this.IsExternal;

  /// <summary>
  /// Link target.
  /// </summary>
  public String Target {
    get => _target;
    set {
      _target = value;
      this.IsExternal = value.StartsWith("/") || value.StartsWith("@") || value.Contains("://");
    }
  }
  private String _target = "";


  public Link() { }
  /// <inheritdoc />
  public Link(LinkInline link) : base(link) {
    this.Target = link.Url!;
  }
}