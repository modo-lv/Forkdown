using System;
using Forkdown.Core.Build;
using Forkdown.Html.Forkdown.Workers;
using Forkdown.Html.Main;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

#pragma warning disable 1591

namespace Forkdown.Html.Wiring; 

public static class HtmlDependencies {
  public static readonly Action<IServiceCollection> Config = svc => {
    // Add HTML workers to the page builder.
    svc.RemoveAll(typeof(ForkdownBuild));
    svc.AddSingleton(
      ForkdownBuild.Default
        .AddWorker<InternalLinkWorker>()
        .AddWorker<SettingsToClassesWorker>()
    );

    svc.AddScoped<AssetBuilder>();
    svc.AddScoped<HtmlProject>();
    svc.AddScoped<HtmlBuilder>();
    svc.AddScoped<JsBuilder>();
    svc.AddScoped<SassBuilder>();
  };
}