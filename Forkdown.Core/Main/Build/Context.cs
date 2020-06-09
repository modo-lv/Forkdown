using System;
using Forkdown.Core.Config;
using Forkdown.Core.Elements;

namespace Forkdown.Core.Build {
  public class Context {
    public BuildConfig? Config { get; set; }

    public Document Document { get; set; }
    
    public Object? DocumentStorage { get; set; }

    public BuilderStorage BuildStore { get; set; }
    
    
    
    public Context(Document doc, BuilderStorage buildStore) {
      this.Document = doc;
      BuildStore = buildStore;
    }

    public Context(Context source) : this(source.Document, source.BuildStore) {
      this.Config = source.Config;
      this.DocumentStorage = source.DocumentStorage;
    }
    
    
    public T Doc<T>() where T : new() =>
      (T) (this.DocumentStorage ??= new T());
  }
}
