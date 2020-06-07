using System;
using Fluent.IO;

namespace Forkdown.Core {
  public class ProjectPath {
    public readonly Path Relative;
    public readonly Path Root;
    public readonly Path Path;


    public ProjectPath(Path root, Path path) {
      this.Root = root;
      this.Relative = path.MakeRelativeTo(root);
      this.Path = this.Root.Combine(this.Relative);
    }


    public override String ToString() => this.RelPathString();
    public String RelPathString() => Relative.ToString().Replace('\\', '/');
    public String FullPathString() => this.Path.FullPath;
  }
}
