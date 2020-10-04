using System;
using Forkdown.Core.Elements.Types;

namespace Forkdown.Core.Elements {
  public class LineBreak : Element, Inline {
    /**
     * Lines following explicit line-breaks 
     */
    public Boolean IsExplicit;
  }
}
