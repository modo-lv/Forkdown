using System;
using System.Reflection;
using Fluent.IO;
using Forkdown.Core.Internal;
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
    /// <summary>
    /// Location of files for HTML output.
    /// </summary>
    public static Path InPath = 
      Path.Get(Assembly.GetExecutingAssembly().Location).Parent().Combine("Resources/Output");

    public const String OutFolder = "out-net";
    
    

    private static void Main(String argument) {
      var services = new ServiceCollection()
        .AddSingleton(new BuildArguments(projectRoot: argument))
        .Config(CoreDependencies.Config)
        .Config(HtmlDependencies.Config)
        .AddLogging(Logging.Config)
        .BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true });
      Core.Program.Services = services;

      var logger = services.GetRequiredService<ILogger<Program>>();

      Console.WriteLine();

      try {
        using var scope = services.CreateScope();
        scope.Service<HtmlBuilder>().Build();
        scope.Service<JsBuilder>().Build();
        scope.Service<CssBuilder>().Build();

        logger.LogInformation("All done.");
      }
      catch (Exception ex) {
        logger.LogCritical(ex, "");
      }
    }
  }
}
