using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Forkdown.Core;

/// <summary>
/// Represents a single Forkdown project.
/// </summary>
/// <remarks>
/// A Forkdown project is, at its core, just a set of Markdown pages to be turned into some output (for example, HTML).
/// </remarks>
public class Project {

  /// <summary>
  /// Path to the root folder that the project files are in.
  /// </summary>
  public readonly IDirectoryInfo Root;

  /// <summary>
  /// Service provider for this project.
  /// </summary>
  public IServiceProvider Services = new ServiceCollection().BuildServiceProvider();

  /// <inheritdoc cref="Project"/>
  /// <param name="root"><see cref="Root"/></param>
  public Project(IDirectoryInfo root) {
    this.Root = root;
  }
}