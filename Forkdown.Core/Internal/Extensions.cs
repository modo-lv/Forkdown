using System;

namespace Forkdown.Core.Internal {
  public static class Extensions {
    public static TTo As<TTo>(this Object subject) {
      return (TTo)subject;
    }
  }
}