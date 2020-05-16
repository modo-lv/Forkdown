using System;

namespace Forkdown.Core.Internal {
  public static class Extensions {
    public static TTo As<TTo>(this Object subject) {
      return (TTo)subject;
    }

    public static String? NonEmpty(this String? str) {
      return String.IsNullOrEmpty(str) ? null : str;
    }

    public static String? NonBlank(this String? str) {
      return str.IsBlank() ? null : str;
    }

    public static Boolean IsBlank(this String? str) {
      return String.IsNullOrWhiteSpace(str);
    }
  }
}