using System;
using Microsoft.Extensions.DependencyInjection;

namespace Forkdown.Core.Wiring.Dependencies; 

public static class Extensions {
  public static IServiceCollection Config(this IServiceCollection? svc, Action<IServiceCollection> configure) {
    svc ??= new ServiceCollection();
    configure(svc);
    return svc;
  }
}