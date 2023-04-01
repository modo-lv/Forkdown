using System.IO.Abstractions;

namespace Forkdown.Core;

/// <summary>
/// Represents a single Forkdown project.
/// </summary>
/// <remarks>
/// A Forkdown project is, at its core, just a set of Markdown pages to be turned into some output (for example, HTML).
/// </remarks>
/// <param name="Root">Path to the root folder that the project files are in.</param>
public record Project(IDirectoryInfo Root);