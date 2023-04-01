using Microsoft.Extensions.DependencyInjection;

namespace Forkdown.Core.Internal; 

public static class ServiceExtensions {
  public static T Service<T>(this IServiceScope provider) where T: notnull =>
    provider.ServiceProvider.GetRequiredService<T>();
}