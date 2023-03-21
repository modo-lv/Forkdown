using System;

namespace Forkdown.Core.Config.Input; 

public abstract class ConfigInput<T> {
  public T ToConfig() {
    return (T)Activator.CreateInstance(typeof(T), this)!;
  }
}