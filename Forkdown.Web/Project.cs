using System.IO.Abstractions;

namespace Forkdown.Web;

/// <summary>
/// Extends a Forkdown <see cref="Core.Project"/> with metadata needed for compiling it to a website. 
/// </summary>
public class Project : Core.Project {

  /// <summary>
  /// Root directory of the generated output files.
  /// </summary>
  public readonly IDirectoryInfo Output;

  /// <inheritdoc />
  public Project(
    IDirectoryInfo root,
    IDirectoryInfo? output = null
  ) : base(root) {
    this.Output = output ?? root.FileSystem.DirectoryInfo.New("out-web");
  }
};