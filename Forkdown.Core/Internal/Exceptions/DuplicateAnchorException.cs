using System;

namespace Forkdown.Core.Internal.Exceptions {
  public class DuplicateAnchorException : Exception {
    public DuplicateAnchorException(String message) : base(message) { }

  }
}
