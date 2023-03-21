using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements.Attributes;
using Markdig.Renderers.Html;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Forkdown.Core.Elements; 

public class ElementAttributes {
  // ReSharper disable once FieldCanBeMadeReadOnly.Global
  public readonly ISet<String> Classes = Nil.SStr;

  public readonly IDictionary<String, String> Properties = Nil.DStr<String>();

  public readonly ElementSettings Settings = new ElementSettings();

  public ElementAttributes() { }

  public ElementAttributes(HtmlAttributes attrs, Element element) {
    if (attrs.Id is { } id && id.NotBlank())
      element.ExplicitIds = new[] { id };
    this.Classes = attrs.Classes?.ToHashSet() ?? Nil.SStr;
    attrs.Properties?.ForEach(prop => {
      if (prop.Key.StartsWith(":"))
        this.Settings[prop.Key[1..]] = prop.Value!;
      else
        this.Properties.Add(prop!);
    });
  }
}