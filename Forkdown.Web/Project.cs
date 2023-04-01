using System.IO.Abstractions;

namespace Forkdown.Web; 

/// <summary>
/// Extends a Forkdown <see cref="Core.Project"/> with metadata needed for compiling it to a website. 
/// </summary>
public class Project : Core.Project {
  /// <inheritdoc />
  public Project(IDirectoryInfo root) : base(root) {
    
  }
};