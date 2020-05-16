using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
#pragma warning disable 1591

namespace Forkdown.Html.Wiring {
  public class Logging {
    public static Action<ILoggingBuilder> Config = cfg => {
      cfg.AddSerilog(new LoggerConfiguration()
        .ReadFrom.Configuration(new ConfigurationBuilder()
          .AddJsonFile("appsettings.json")
          .AddEnvironmentVariables()
          .Build()
        )
        .CreateLogger()
      );
    };
  }
}