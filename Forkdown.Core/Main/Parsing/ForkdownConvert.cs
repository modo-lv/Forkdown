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
  public static class ForkdownConvert {
    public static Document ToDocument(Path path, String? fileName = null) {
      using var reader = new FileInfo(path.FullPath).OpenText();
      return ForkdownConvert.ToDocument(reader.ReadToEnd(), fileName ?? path.ToString());
    }

    public static Document ToDocument(String markdown, String fileName = "") {
      MarkdownDocument mDoc = MarkdownParse.ToDocument(markdown);
      var result = (Document) ForkdownConvert.ToElement(mDoc);
      result.FileName = fileName;
      return result;
    }

    public static Element ToElement(MarkdownObject node) {
      Element result = node switch
      {
        MarkdownDocument d => new Document(d),
        HeadingBlock h => new Heading(h),
        ParagraphBlock p => new Paragraph(p),
        EmphasisInline e => new Emphasis(e),
        CodeInline c => new Code(c),
        LiteralInline t => new Text(t),
        _ => new Placeholder(node),
      };

      IEnumerable<MarkdownObject> subs = node switch
      {
        LeafBlock b => b.Inline ?? Enumerable.Empty<MarkdownObject>(),
        IEnumerable<MarkdownObject> e => e,
        _ => Enumerable.Empty<MarkdownObject>()
      };

      result.Subs = subs.Select(ForkdownConvert.ToElement).ToList();

      return result;
    }
  }
}