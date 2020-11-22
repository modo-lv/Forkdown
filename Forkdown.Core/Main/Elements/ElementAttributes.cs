using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements.Attributes;
using Markdig.Renderers.Html;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Forkdown.Core.Elements {
  public class ElementAttributes {
    // ReSharper disable once FieldCanBeMadeReadOnly.Global
    public readonly ISet<String> Classes = Nil.SStr;

    public readonly IDictionary<String, String> Properties = Nil.DStr<String>();

    public readonly ElementSettings Settings = new ElementSettings();

    public ElementAttributes() { }

    public ElementAttributes(HtmlAttributes attrs, Element element) {
      if (attrs.Id.NotBlank())
        element.ExplicitIds = new[] { attrs.Id };
      this.Classes = attrs.Classes?.ToHashSet() ?? Nil.SStr;
      attrs.Properties?.ForEach(_ => {
        if (_.Key.StartsWith(":"))
          this.Settings[_.Key.Substring(1)] = _.Value;
        else
          this.Properties.Add(_);
      });
    }
  }
}
