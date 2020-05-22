using System;
using System.Collections.Generic;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Parsing.Forkdown {
  public partial class InternalLinks {
    public static InternalLinks From(params Document[] docs) =>
      From(docs as IEnumerable<Document>);

    /// <summary>
    /// Build a local link index from Forkdown documents.
    /// </summary>
    /// <param name="docs">Forkdown documents, with global IDs processed.</param>
    public static InternalLinks From(IEnumerable<Document> docs) {
      var index = new InternalLinks();

      foreach (var doc in docs)
        addToIndex(doc);

      void addToIndex(Element el, Document? doc = null) {
        doc ??= (Document) el;

        index.Add(el.GlobalId, doc);
        el.Subs.ForEach(_ => addToIndex(_, doc));
      }

      return index;
    }

    public static KeyValuePair<String, Document> InternalLink(KeyValuePair<String, Document> item) =>
      new KeyValuePair<String, Document>(GlobalId.From(item.Key), item.Value);      
    
    
  }
}
