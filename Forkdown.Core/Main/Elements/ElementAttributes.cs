using System;
using System.Collections.Generic;
using System.Linq;
using Markdig.Renderers.Html;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Main.Elements {
  public class ElementAttributes {
    public String Id;

    public ISet<String> Classes;

    private IDictionary<String, String> _properties = new Dictionary<String, String>();
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
      this.Classes = attrs.Classes?.ToHashSet() ?? new HashSet<String>();
      this.Properties = attrs.Properties?.ToDictionary(_ => _.Key, _ => _.Value)
        ?? new Dictionary<String, String>();
    }
  }
}