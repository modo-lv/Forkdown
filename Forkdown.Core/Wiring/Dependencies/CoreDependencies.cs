using System;
using Forkdown.Core.Main;
using Microsoft.Extensions.DependencyInjection;

namespace Forkdown.Core.Wiring.Dependencies {
  public static class CoreDependencies {
    public static readonly Action<IServiceCollection> Config = svc => {
      svc.AddScoped<Project>();
    };
  }
}