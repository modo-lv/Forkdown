﻿using System;
using System.Collections.Generic;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Build {
  public class Arguments : Dictionary<String, Object?> {

    public Arguments() { }
    public Arguments(IDictionary<String, Object?> dictionary) : base(dictionary) { }


    public String Get() => this.GetOr("");

    public T GetOr<T>(T fallback) => (T) this.GetOrAdd("", fallback)!;

    public T Get<T>(String key = "") where T : notnull => (T) this.GetOrAdd(key, default(T))!;

    public void Put<T>(T value) => this[""] = value;

    public void Put<T>(String key, T value) => this[key] = value;

  }
}
