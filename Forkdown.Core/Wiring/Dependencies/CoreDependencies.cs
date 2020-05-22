using System;
using Forkdown.Core.Parsing.Forkdown;
using Microsoft.Extensions.DependencyInjection;

namespace Forkdown.Core.Wiring.Dependencies {
  public static class CoreDependencies {
    public static readonly Action<IServiceCollection> Config = svc => {
      svc.AddSingleton(ForkdownBuilder.Default);
      svc.AddScoped<Project>();
    };
  }
}