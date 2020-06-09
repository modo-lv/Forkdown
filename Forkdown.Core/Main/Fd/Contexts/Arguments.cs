﻿using System;
using System.Collections.Generic;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Fd.Contexts {
  public class Arguments : Dictionary<String, Object?> {

    public Arguments() { }
    public Arguments(IDictionary<String, Object?> dictionary) : base(dictionary) { }


    public String Get() => this.GetOr("");

    public T GetOr<T>(T fallback) => (T) this.GetOrAdd("", fallback)!;

    public T Get<T>() where T : notnull => (T) this.GetOrAdd("", default(T))!;

    public void Put<T>(T value) => this[""] = value;
    
    public void Put<T>(String key, T value) => this[key] = value;

  }
}
