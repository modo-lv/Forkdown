using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Forkdown.Core.Main.Elements;
using Markdig;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Path = Fluent.IO.Path;

namespace Forkdown.Core.Main.Parsing {
  public static class ForkdownObject {
    public static Document ToDocument(Path path, String? fileName = null) {
      using var reader = new FileInfo(path.FullPath).OpenText();
      return ForkdownObject.ToDocument(reader.ReadToEnd(), fileName ?? path.ToString());
    }
    
    public static Document ToDocument(String markdown, String fileName = "") {
      MarkdownDocument mDoc = Markdown.Parse(markdown);
      var result = (Document) ForkdownObject.ToElement(mDoc);
      result.FileName = fileName;
      return result;
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