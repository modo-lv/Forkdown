using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Parsing {
  public partial class AnchorIndex {
    /// <summary>
    /// Build an anchor index from Forkdown documents.
    /// </summary>
    /// <param name="docs">Forkdown documents, with anchors processed.</param>
    public static AnchorIndex BuildFrom(IEnumerable<Document> docs) {
      var index = new AnchorIndex();

      foreach (Document doc in docs) findAnchors(doc); void findAnchors(Element el, Document? doc = null) {
        doc ??= (Document) el;

        var anchors = el.Attributes.Classes
          .Prepend(el.Attributes.Id)
          .Where(_ => _.StartsWith("#") || _ == "#~")
          .ToList();

        foreach (var anchor in anchors)
        {
          var a = Anchor(anchor == "#~" ? el.Title : anchor.Part(1));
          index.Add(a, doc);
          el.Anchor = a;

          if (el.Attributes.Id.IsBlank() && a.NotBlank())
            el.Attributes.Id = a;
        }

        foreach (Element sub in el.Subs)
          findAnchors(sub, doc);
      }

      return index;
    }

    public static KeyValuePair<String, Document> Anchor(KeyValuePair<String, Document> item) =>
      new KeyValuePair<String, Document>(AnchorIndex.Anchor(item.Key), item.Value);

    public static String Anchor(String input) =>
      Regex.Replace(input.Text().Trim().Replace(" ", "_"), @"[^\w_-]", "").ToLowerInvariant();
  }
}
