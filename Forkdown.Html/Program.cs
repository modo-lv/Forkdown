using System;
using Forkdown.Core;
using Forkdown.Core.Wiring;
using Forkdown.Core.Wiring.Dependencies;
using Forkdown.Html.Main;
using Forkdown.Html.Wiring;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
      Core.Program.Services = services;
      var logger = services.GetRequiredService<ILogger<Program>>();

      Console.WriteLine();

      try
      {
        using var scope = services.CreateScope();
        
        var project = scope.ServiceProvider.GetRequiredService<Project>();
        var html = scope.ServiceProvider.GetRequiredService<HtmlOutput>();

        project.Load();
        html.Build();
      }
      catch (Exception ex)
      {
        logger.LogCritical(ex, "");
      }
    }
  }
}