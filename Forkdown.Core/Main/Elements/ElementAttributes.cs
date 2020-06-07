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
    public String Id = "";

    // ReSharper disable once FieldCanBeMadeReadOnly.Global
    public ISet<String> Classes = Nil.SStr;

    public IDictionary<String, String> Properties = Nil.DStr<String>();

    public String ClassesString => this.Classes.StringJoin(" ") ?? "";

    public String PropertiesString => this.Properties.Select(_ => $"{_.Key}=\"{_.Value}\"").StringJoin(" ");

   
    
    public ElementAttributes() { }

    public ElementAttributes(HtmlAttributes attrs, ElementSettings settings) {
      this.Id = attrs.Id.Text();
      this.Classes = attrs.Classes?.ToHashSet() ?? Nil.SStr;
      attrs.Properties?.ForEach(_ => {
        if (_.Key.StartsWith(":"))
          settings[_.Key.Part(1)] = _.Value;
        else
          this.Properties.Add(_);
      });
    }
  }
}
