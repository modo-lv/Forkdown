using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Main.Elements;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Simpler.NetCore.Collections;
using Path = Fluent.IO.Path;

namespace Forkdown.Core.Parsing {
  /// <summary>
  /// Main class for converting Markdown to Forkdown.
  /// </summary>
  public static class Forkdown {

    /// <summary>
    /// An overload for <see cref="FromMarkdown(String,String)"/>, for parsing Markdown directly from a file.
    /// </summary>
    /// <param name="file">File to parse.</param>
    /// <param name="fileName">File name to set <see cref="Document.FileName"/> to.</param>
    public static Document FromMarkdown(Path file, String? fileName = null) {
      using var reader = new FileInfo(file.FullPath).OpenText();
      return Forkdown.FromMarkdown(reader.ReadToEnd(), fileName ?? file.ToString());
    }

    /// <summary>
    /// Parse a Markdown file into a Forkdown document.
    /// </summary>
    /// <param name="markdown">Markdown text to parse.</param>
    /// <param name="fileName">File name to set <see cref="Document.FileName"/> to.</param>
    public static Document FromMarkdown(String markdown, String fileName = "") {
      MarkdownDocument mDoc = Markdown.ToDocument(markdown);
      var result = (Document) Forkdown.FromMarkdown(mDoc);
      result.FileName = fileName;
      return result;
    }

    /// <summary>
    /// Convert a parsed Markdown object into the corresponding Forkdown element. 
    /// </summary>
    /// <param name="mdo"></param>
    /// <returns></returns>
    public static Element FromMarkdown(MarkdownObject mdo) {
      Element result = mdo switch
      {
        MarkdownDocument d => new Document(d),
        HeadingBlock h => new Heading(h),
        ParagraphBlock p => new Paragraph(p),
        EmphasisInline e => new Emphasis(e),
        CodeInline c => new Code(c),
        LinkInline l => new Link(l),
        LiteralInline t => new Text(t),
        _ => new Placeholder(mdo),
      };

      var subs = mdo switch
      {
        LeafBlock b => b.Inline,
        IEnumerable<MarkdownObject> e => e,
        _ => null
      } ?? Nil.E<MarkdownObject>();

      result.Subs = subs.Select(Forkdown.FromMarkdown).ToList();

      return result;
    }
  }
}
