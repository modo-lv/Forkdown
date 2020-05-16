using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Forkdown.Core.Main.Elements;
using Markdig;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace Forkdown.Core.Main.Parsing {
  public static class ForkdownObject {
    public static Document ToDocument(FileInfo file) {
      using var reader = file.OpenText();
      return ForkdownObject.ToDocument(reader.ReadToEnd());
    }
    
    public static Document ToDocument(String markdown) {
      MarkdownDocument mDoc = Markdown.Parse(markdown);
      return (Document) ForkdownObject.ToElement(mDoc);
    }

    public static Element ToElement(MarkdownObject node) {
      Element result = node switch
      {
        MarkdownDocument d => new Document(d),
        HeadingBlock h => new Heading(h),
        LiteralInline t => new Text(t),
        _ => new Placeholder(node),
      };

      IEnumerable<MarkdownObject> subs = node switch
      {
        LeafBlock b => b.Inline?.AsEnumerable() ?? Enumerable.Empty<MarkdownObject>(),
        ContainerBlock b => b.AsEnumerable(),
        _ => Enumerable.Empty<MarkdownObject>()
      };

      result.Subs = subs.Select(ForkdownObject.ToElement).ToList();

      return result;
    }
  }
}