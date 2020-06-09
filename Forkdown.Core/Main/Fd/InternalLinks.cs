using System;
using System.Collections;
using System.Collections.Generic;
using Forkdown.Core.Elements;
using Forkdown.Core.Internal.Exceptions;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Fd {
  /// <summary>
  /// A reference collection for tracking which internal link anchor is defined in which document.
  /// </summary>
  public partial class InternalLinks : IDictionary<String, Elements.Document> {

    private readonly IDictionary<String, Elements.Document> _ = Nil.DStr<Elements.Document>();
    
    public Elements.Document this[String key]
    {
      get => this._[GlobalId.From(key)];
      set => this._[GlobalId.From(key)] = value;
    }

    public void Add(KeyValuePair<String, Document> item) {
      item = InternalLink(item);
      if (this.ContainsKey(item.Key))
        throw new DuplicateAnchorException();
      if (item.Key.NotBlank())
        this._.Add(item);
    }

    public Boolean Contains(KeyValuePair<String, Elements.Document> item) =>
      this._.Contains(InternalLink(item));

    public Boolean Remove(KeyValuePair<String, Elements.Document> item) =>
      this._.Remove(InternalLink(item));

    public void Add(String key, Elements.Document value) {
      this.Add(new KeyValuePair<String, Elements.Document>(key, value));
    }

    public Boolean ContainsKey(String key) =>
      this._.ContainsKey(GlobalId.From(key));

    public Boolean Remove(String key) =>
      this._.Remove(GlobalId.From(key));

    public Boolean TryGetValue(String key, out Elements.Document value) =>
#pragma warning disable 8601
      this._.TryGetValue(GlobalId.From(key), out value);
#pragma warning restore 8601
    

    public IEnumerator<KeyValuePair<String, Elements.Document>> GetEnumerator() => this._.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    public void Clear() => this._.Clear();
    public void CopyTo(KeyValuePair<String, Elements.Document>[] array, Int32 arrayIndex) { throw new NotImplementedException(); }
    public Int32 Count => this._.Count;
    public Boolean IsReadOnly => this._.IsReadOnly;
    public ICollection<String> Keys => this._.Keys;
    public ICollection<Elements.Document> Values => this._.Values;
  }
}
