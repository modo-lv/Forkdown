using System;
using System.IO;
using Simpler.Net.FileSystem;

namespace Forkdown.Core.Wiring {
  public static class FileSystemExtensions {
    public static FileInfo File(this DirectoryInfo dir, String fileName) {
      return new FileInfo(Path.Combine(dir.ToString(), fileName));
    }
  }
}