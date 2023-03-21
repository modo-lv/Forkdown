using System;
using System.IO;

namespace Forkdown.Core.Wiring; 

public static class FileSystemExtensions {
  public static FileInfo File(this Fluent.IO.Path path, String fileName) {
    return new FileInfo(path.Combine(fileName).FullPath);
  }
}