using System;
using Forkdown.Core.Build;
using Microsoft.Extensions.DependencyInjection;

namespace Forkdown.Core.Wiring.Dependencies; 

public static class CoreDependencies {
  public static readonly Action<IServiceCollection> Config = svc => {
    svc.AddSingleton(ForkdownBuild.Default);
    svc.AddScoped<Project>();
  };
}