using System;
using System.Collections;
using System.Collections.Generic;
using Forkdown.Core.Elements;
using Forkdown.Core.Internal.Exceptions;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core {
  /// <summary>
  /// A reference collection for tracking which internal link anchor is defined in which document.
  /// </summary>
  public partial class LinkIndex : IDictionary<String, Document> {

    private readonly IDictionary<String, Document> _ = Nil.DStr<Document>();
    
    public Document this[String key]
    {
      get => this._[Globals.Id(key)];
      set => this._[Globals.Id(key)] = value;
    }

    public void Add(KeyValuePair<String, Document> item) {
      item = InternalLink(item);
      if (this.ContainsKey(item.Key))
        throw new DuplicateAnchorException($"Cannot add anchor {item.Key} from {item.Value.ProjectFileId} since it" +
                                           $" already exists in {this[item.Key].ProjectFileId}");
      if (item.Key.NotBlank())
        this._.Add(item);
    }

    public Boolean Contains(KeyValuePair<String, Document> item) =>
      this._.Contains(InternalLink(item));

    public Boolean Remove(KeyValuePair<String, Document> item) =>
      this._.Remove(InternalLink(item));

    public void Add(String key, Document value) {
      this.Add(new KeyValuePair<String, Document>(key, value));
    }

    public Boolean ContainsKey(String key) =>
      this._.ContainsKey(Globals.Id(key));

    public Boolean Remove(String key) =>
      this._.Remove(Globals.Id(key));

    public Boolean TryGetValue(String key, out Document value) =>
#pragma warning disable 8601
      this._.TryGetValue(Globals.Id(key), out value);
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
