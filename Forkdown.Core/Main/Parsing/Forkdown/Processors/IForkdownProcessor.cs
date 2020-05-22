using Forkdown.Core.Elements;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  public interface IForkdownProcessor {
    T Process<T>(T element, Document? doc = null) where T : Element;
  }
}
