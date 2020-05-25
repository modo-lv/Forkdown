using System;
using System.IO;
using Path = Fluent.IO.Path;

namespace Forkdown.Core.Wiring {
  /// <summary>
  /// Parameters for running a build (usually from command line arguments).
  /// </summary>
  public class BuildArguments {
    /// <summary>
    /// Location of the Forkdown project to build.
    /// </summary>
    public readonly Path ProjectRoot;

    /// <summary>
    /// File containing the main project settings.
    /// </summary>
    public readonly FileInfo MainConfigFile;


    /// Constructor 
    public BuildArguments(String projectRoot) {
      this.ProjectRoot = Path.Get(Path.Get(projectRoot).FullPath);
      this.MainConfigFile = this.ProjectRoot.File("forkdown.main.config.yaml");
    }

  }
}
