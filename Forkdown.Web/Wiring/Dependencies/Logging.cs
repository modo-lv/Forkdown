using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Forkdown.Web.Wiring.Dependencies;

/// <summary>
/// Contains configuration for logg output. 
/// </summary>
public static class Logging {
  public static SystemConsoleTheme Forkdown { get; } = new SystemConsoleTheme(
    new Dictionary<ConsoleThemeStyle, SystemConsoleThemeStyle> {
      [ConsoleThemeStyle.Text] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Gray },
      [ConsoleThemeStyle.SecondaryText] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkGray },
      [ConsoleThemeStyle.TertiaryText] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkGray },
      [ConsoleThemeStyle.Invalid] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Yellow },
      [ConsoleThemeStyle.Null] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.White },
      [ConsoleThemeStyle.Name] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.White },
      [ConsoleThemeStyle.String] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Cyan },
      [ConsoleThemeStyle.Number] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Cyan },
      [ConsoleThemeStyle.Boolean] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Cyan },
      [ConsoleThemeStyle.Scalar] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Cyan },
      [ConsoleThemeStyle.LevelVerbose] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Gray },
      [ConsoleThemeStyle.LevelDebug] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.White },
      [ConsoleThemeStyle.LevelInformation] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.White },
      [ConsoleThemeStyle.LevelWarning] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkGray },
      [ConsoleThemeStyle.LevelError] = new SystemConsoleThemeStyle
        { Foreground = ConsoleColor.White, Background = ConsoleColor.Red },
      [ConsoleThemeStyle.LevelFatal] = new SystemConsoleThemeStyle
        { Foreground = ConsoleColor.White, Background = ConsoleColor.Red },
    });

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
      .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u4}] {Message:lj}{Exception}{NewLine}",
        theme: Forkdown
      )
      .CreateLogger()
    );
  };
}