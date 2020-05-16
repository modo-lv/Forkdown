using System;
using Forkdown.Core.Main;
using Forkdown.Core.Wiring;
using Forkdown.Core.Wiring.Dependencies;
using Forkdown.Html.Main;
using Forkdown.Html.Wiring;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedType.Global

namespace Forkdown.Html {
  internal class Program {
    private static void Main(String argument) {
      var services = new ServiceCollection()
        .AddSingleton(new AppArguments(projectRoot: argument))
        .Config(CoreDependencies.Config)
        .Config(HtmlDependencies.Config)
        .AddLogging(Logging.Config)
        .BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true });

      Console.WriteLine();

      services.GetRequiredService<HtmlOutput>().Build();
    }
  }
}