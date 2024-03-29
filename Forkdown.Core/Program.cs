﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Forkdown.Core; 

public class Program {
  private static IServiceProvider? _services;

  public static IServiceProvider Services {
    get => _services ?? throw new NullReferenceException(
      $"Cannot run without an {typeof(Program).GetProperty(nameof(Services))!.PropertyType.Name} " +
      $"instance assigned to {typeof(Program).FullName}.{nameof(Services)}!");
    set => _services = value;
  }

  public static T Service<T>() where T : notnull => Program.Services.GetRequiredService<T>();

  public static ILogger<T> Logger<T>() {
    if (_services != null)
      return Program.Service<ILogger<T>>();
    return LoggerFactory.Create(_ => _.AddConsole()).CreateLogger<T>();
  }
}