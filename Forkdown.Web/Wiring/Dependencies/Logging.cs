using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Forkdown.Web.Wiring.Dependencies;

public class Logging {
  /// <summary>
  /// Configure logging for the app.
  /// </summary>
  public static Action<ILoggingBuilder> Config = cfg => {
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