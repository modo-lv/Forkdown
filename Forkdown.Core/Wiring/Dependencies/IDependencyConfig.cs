using Microsoft.Extensions.DependencyInjection;

namespace Forkdown.Core.Wiring.Dependencies {
  public interface IDependencyConfig {
    IServiceCollection Configure(IServiceCollection svc);
  }
}