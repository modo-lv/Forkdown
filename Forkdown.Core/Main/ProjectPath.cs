using System;
using System.Collections.Generic;
using System.Linq;
using Fluent.IO;

namespace Forkdown.Core {
  public class ProjectPath {
    public readonly Path Relative;
    public readonly Path Root;
    public readonly Path Path;


    public ProjectPath(Path root, Path path) {
      this.Root = root;
      this.Relative = path.IsRooted ? path.MakeRelativeTo(root) : path;
      this.Path = this.Root.Combine(this.Relative);
    }


    public Boolean Exists => this.Path.Exists;
    public override String ToString() => this.RelPathString();
    public String RelPathString() => Relative.ToString().Replace('\\', '/');
    public String FullPathString() => this.Path.FullPath.Replace('\\', '/');

    public IEnumerable<ProjectPath> Files(String filter, Boolean recursive) => 
      this.Path.Files(filter, recursive).Select(_ => new ProjectPath(this.Root, _));
    public Path Combine(String path) => this.Path.Combine(path);
  }
}
