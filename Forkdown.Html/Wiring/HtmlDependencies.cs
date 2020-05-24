using System;
using Forkdown.Html.Main;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable 1591

namespace Forkdown.Html.Wiring {
  public static class HtmlDependencies {
    public static readonly Action<IServiceCollection> Config = svc => {
      svc.AddScoped<JsBuilder>();
      svc.AddScoped<CssBuilder>();
      svc.AddScoped<HtmlBuilder>();
    };
  }
}