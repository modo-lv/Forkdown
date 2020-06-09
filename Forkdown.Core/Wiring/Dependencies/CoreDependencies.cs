using System;
using Forkdown.Core.Fd;
using Microsoft.Extensions.DependencyInjection;

namespace Forkdown.Core.Wiring.Dependencies {
  public static class CoreDependencies {
    public static readonly Action<IServiceCollection> Config = svc => {
      svc.AddSingleton(FdBuilder.Default);
      svc.AddScoped<Project>();
    };
  }
}