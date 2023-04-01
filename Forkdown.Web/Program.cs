using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.IO.Abstractions;
using Simpler.Net.Main;

namespace Forkdown.Web;

internal class Program {
  private static void Main(String[] args) {
    Startup(project => {
      
    }).Invoke(args);
  }

  private static RootCommand Startup(Action<Project> run) =>
    new RootCommand("Compile a Forkdown project into an interactive, standalone web site.") {
        new Argument<DirectoryInfo>("source",
          "Location (root directory) where the project files are located."
        )
      }
      .Also(it => {
        it.Handler = CommandHandler.Create<DirectoryInfo>(source =>
          run(new Project(
            root: new FileSystem().DirectoryInfo.Wrap(source))
          )
        );
      });
}