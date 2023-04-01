using System.IO.Abstractions;

namespace Forkdown.Web; 

/// <summary>
/// Extends a Forkdown <see cref="Core.Project"/> with metadata needed for compiling it to a website. 
/// </summary>
public record Project(IDirectoryInfo Root) : Core.Project(Root);