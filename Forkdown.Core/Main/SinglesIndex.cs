using System;
using System.Collections.Generic;

namespace Forkdown.Core; 

/// <summary>
/// Index of singleton elements, keyed by ID.
/// </summary>
public class SinglesIndex : Dictionary<String, ICollection<String>> {
}