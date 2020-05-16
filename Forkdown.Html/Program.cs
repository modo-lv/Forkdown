using System;
using System.IO;
using Forkdown.Core.Main;
using Forkdown.Core.Wiring.Dependencies;
using Forkdown.Html.Wiring;
using Forkdown.Html.Wiring.Dependencies;
using Microsoft.Extensions.DependencyInjection;
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedType.Global

namespace Forkdown.Html {
  internal class Program {
    private static void Main(String argument) {

      var services = new ServiceCollection()
        .Config(CoreDependencies.Config)
        .Config(HtmlDependencies.Config)
        .AddLogging(Logging.Config)
        .BuildServiceProvider();
      
      Console.WriteLine();
      
      using (services.CreateScope())
      {
        var project = services.GetRequiredService<Project>();
        project.Dir = new DirectoryInfo(argument);
        project.Build();
      }
    }
  }
}