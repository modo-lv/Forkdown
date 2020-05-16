using System;
using Forkdown.Core.Wiring.Dependencies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Forkdown.Core {
  public class Program {

    public static IServiceProvider Services = new ServiceCollection()
      .Config(CoreDependencies.Config)
      .BuildServiceProvider();

    public static T Service<T>() => Program.Services.GetRequiredService<T>();

    public static ILogger<T> Logger<T>() => Program.Service<ILogger<T>>();
    
  }
}