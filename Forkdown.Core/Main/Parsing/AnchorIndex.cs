using System;
using System.Collections;
using System.Collections.Generic;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;
using YamlDotNet.Core.Tokens;

namespace Forkdown.Core.Parsing {
  /// <summary>
  /// A reference collection for tracking which anchor is defined in which document.
  /// </summary>
  public partial class AnchorIndex : IDictionary<String, Document> {

    private readonly IDictionary<String, Document> _ = Nil.DStr<Document>();
    
    public Document this[String key]
    {
      get => this._[Anchor(key)];
      set => this._[Anchor(key)] = value;
    }

    public void Add(KeyValuePair<String, Document> item) {
      item = Anchor(item);
      if (item.Key.NotBlank())
        this._.Add(item);
    }

    public Boolean Contains(KeyValuePair<String, Document> item) =>
      this._.Contains(Anchor(item));

    public Boolean Remove(KeyValuePair<String, Document> item) =>
      this._.Remove(Anchor(item));

    public void Add(String key, Document value) {
      key = Anchor(key);
      if (key.NotBlank())
        this._.Add(key, value);
    }

    public Boolean ContainsKey(String key) =>
      this._.ContainsKey(Anchor(key));

    public Boolean Remove(String key) =>
      this._.Remove(Anchor(key));

    public Boolean TryGetValue(String key, out Document value) =>
#pragma warning disable 8601
      this._.TryGetValue(Anchor(key), out value);
#pragma warning restore 8601
    

    public IEnumerator<KeyValuePair<String, Document>> GetEnumerator() => this._.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    public void Clear() => this._.Clear();
    public void CopyTo(KeyValuePair<String, Document>[] array, Int32 arrayIndex) { throw new NotImplementedException(); }
    public Int32 Count => this._.Count;
    public Boolean IsReadOnly => this._.IsReadOnly;
    public ICollection<String> Keys => this._.Keys;
    public ICollection<Document> Values => this._.Values;
  }
}
