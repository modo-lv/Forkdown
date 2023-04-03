using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using System.IO;
using System.IO.Abstractions;
using System.Reflection;
using System.Threading;
using Forkdown.Core;
using Forkdown.Core.Config;
using Forkdown.Web.Wiring.Dependencies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Simpler.Net.Main;

namespace Forkdown.Web;

// ReSharper disable once UnusedType.Global
internal class Program {
  // ReSharper disable once UnusedMember.Local
  private static void Main(String[] args) {
    Global.ServiceProvider = new ServiceCollection()
      .AddLogging(Logging.Config)
      .BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true });

    Startup( project => {
        // Dependencies
        var logger = Global.Service<ILogger<Program>>();

        Assembly.GetExecutingAssembly().GetName().Also(it =>
          logger.LogInformation("Running {Assembly} {Version} on project {Project}...", it.Name, it.Version, project.CoreConfig.Name)
        );
        logger.LogInformation("Running project in: {Root}", project.Root.FullName);
        logger.LogInformation("Output will go to: {Output}", project.Output.FullName);
      })
      .Invoke(args)
      .Also(e => {
        if (e != 0) Thread.Sleep(1000);
      });
  }

  private static RootCommand Startup(Action<Project> run) =>
    new RootCommand("Compile a Forkdown project into an interactive, standalone web site.") {
        new Argument<DirectoryInfo>("source",
          "Location (root directory) where the project source files (Forkdown pages) are located."
        ),
        new Option<DirectoryInfo>(new[] { "--output", "-o" },
          "Location (root directory) where the generated web site should be placed.").Also(
          it => {
            it.IsRequired = false;
            it.SetDefaultValue(Project.DefaultOutput);
            it.ArgumentHelpName = "path";
          }),
        new Option<FileInfo>(new[] { "--core-config", "-cc" },
          "Path to the core configuration file.").Also(
          it => {
            it.IsRequired = false;
            it.SetDefaultValue(Core.Project.DefaultCoreConfigFileName);
            it.ArgumentHelpName = "path";
          })
      }
      .Also(command =>
        command.Handler = CommandHandler.Create<DirectoryInfo, DirectoryInfo?, FileInfo?>((source, output, cc) =>
          new FileSystem().Also(fs => {
            run(new Project(
              root: fs.DirectoryInfo.Wrap(source),
              coreConfigFile: cc?.Let(fs.FileInfo.Wrap) ?? fs.FileInfo.New(Core.Project.DefaultCoreConfigFileName),
              output: output?.Let(fs.DirectoryInfo.Wrap) ?? fs.DirectoryInfo.New(Project.DefaultOutput)
            ));
          })
        )
      );
}