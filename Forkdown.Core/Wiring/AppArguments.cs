using System;

namespace Forkdown.Core.Wiring {
  public class AppArguments {
    public String ProjectRoot;
    public AppArguments(String projectRoot) { this.ProjectRoot = projectRoot; }
  }
}