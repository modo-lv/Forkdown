using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Forkdown.Web.Wiring.Dependencies;

/// <summary>
/// Contains configuration for logg output. 
/// </summary>
public static class Logging {
  /// <summary>
  /// Configure logging for the app.
  /// </summary>
  public static readonly Action<ILoggingBuilder> Config = cfg => {
    cfg.AddSerilog(new LoggerConfiguration()
      .ReadFrom
      .Configuration(new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build()
      )
      .CreateLogger()
    );
  };
}