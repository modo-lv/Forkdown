using Microsoft.Extensions.DependencyInjection;

namespace Forkdown.Core; 

/// <summary>
/// Globally available functionality. 
/// </summary>
public static class Global {
  private static IServiceProvider? _serviceProvider;
  /// <summary>
  /// Global service provider.
  /// </summary>
  /// <remarks>
  /// <see cref="Project"/>s create their own service provider scopes, this is only meant to be used for global stuff
  /// like loggers.
  /// </remarks>
  /// <exception cref="NullReferenceException">
  /// If no service provider has been set by the time the first service resolution is attempted.
  /// </exception>
  public static IServiceProvider ServiceProvider {
    get => _serviceProvider ?? throw new NullReferenceException($"{nameof(Global)}.{nameof(ServiceProvider)}");
    set => _serviceProvider = value;
  }

  /// <summary>
  /// Resolve a global service reference.
  /// </summary>
  /// <typeparam name="T">Type of service to resolve.</typeparam>
  /// <returns>An instance of the requested service.</returns>
  public static T Service<T>() where T : notnull => 
    ServiceProvider.GetRequiredService<T>();
}