using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using System.IO;
using System.IO.Abstractions;
using Forkdown.Web.Wiring.Dependencies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Simpler.Net.Main;

namespace Forkdown.Web;

// ReSharper disable once UnusedType.Global
internal class Program {
  // ReSharper disable once UnusedMember.Local
  private static void Main(String[] args) {
    Startup(project => {
      // Dependencies
      var logger =
        new ServiceCollection()
          .AddLogging(Logging.Config)
          .BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true })
          .Also(provider => project.Services = provider)
          .GetRequiredService<ILogger<Program>>();
    }).Invoke(args);
  }

  private static RootCommand Startup(Action<Project> run) =>
    new RootCommand("Compile a Forkdown project into an interactive, standalone web site.") {
        new Argument<DirectoryInfo>("source",
          "Location (root directory) where the project source files (Forkdown pages) are located."
        ),
        new Option<DirectoryInfo>(new[] { "--output", "-o" },
            "Location (root directory) where the generated web site should be placed.")
          .Also(it => {
            it.IsRequired = false;
            it.SetDefaultValue(Project.DefaultOutput);
            it.ArgumentHelpName = "path";
          })
      }
      .Also(command =>
        command.Handler = CommandHandler.Create<DirectoryInfo, DirectoryInfo>((source, output) =>
          new FileSystem().Also(fs => {
            run(new Project(
              root: fs.DirectoryInfo.Wrap(source),
              output: fs.DirectoryInfo.Wrap(output)
            ));
          })
        )
      );
}