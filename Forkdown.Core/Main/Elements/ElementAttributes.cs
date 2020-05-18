using System;
using System.Collections.Generic;
using System.Linq;
using Markdig.Renderers.Html;
using Simpler.NetCore.Collections;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Forkdown.Core.Elements {
  public class ElementAttributes {
    public String Id;

    // ReSharper disable once FieldCanBeMadeReadOnly.Global
    public ISet<String> Classes;

    private IDictionary<String, String> _properties = Nil.DStr<String>();
    public IDictionary<String, String> Properties
    {
      get => this._properties;
      set
      {
        this._properties = value;
        this.PropertiesString = value.Select(_ => $"{_.Key}=\"{_.Value}\"").StringJoin(" ");
      }
    }

    public String ClassesString => this.Classes.StringJoin(" ") ?? "";
    public String PropertiesString { get; protected set; } = "";

    public ElementAttributes(HtmlAttributes attrs) {
      this.Id = attrs.Id;
      this.Classes = attrs.Classes?.ToHashSet() ?? Nil.SStr;
      this.Properties =
        attrs.Properties?.ToDictionary(_ => _.Key, _ => _.Value) ?? Nil.DStr<String>();
    }
  }
}
