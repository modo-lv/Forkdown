using System;
using System.Collections.Generic;
using Forkdown.Core.Elements;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  /// <summary>
  /// A Forkdown processor that handles one element at a time.
  /// </summary>
  public interface IElementProcessor {
    /// <param name="element">Element to process.</param>
    /// <param name="args">Arguments passed from the parent element processor.</param>
    T Process<T>(T element, IDictionary<String, Object> args) where T : Element;
  }
}
