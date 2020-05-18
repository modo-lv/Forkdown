using System;
using Microsoft.Extensions.DependencyInjection;

namespace Forkdown.Core.Internal {
  public static class Extensions {
    public static T Service<T>(this IServiceScope provider) =>
      provider.ServiceProvider.GetRequiredService<T>();
  }
}
