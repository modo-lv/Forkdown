using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Forkdown.Core.Elements;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Simpler.NetCore.Collections;
using Path = Fluent.IO.Path;

namespace Forkdown.Core.Parsing {
  /// <summary>
  /// Main class for converting Markdown to Forkdown.
  /// </summary>
  public static class BuildForkdown {

    /// <summary>
    /// An overload for <see cref="From(string,string)"/>, for parsing Markdown directly from a file.
    /// </summary>
    /// <param name="file">File to parse.</param>
    /// <param name="fileName">File name to set <see cref="Document.FileName"/> to.</param>
    public static Document From(Path file, String? fileName = null) {
      using var reader = new FileInfo(file.FullPath).OpenText();
      return BuildForkdown.From(reader.ReadToEnd(), fileName ?? file.ToString());
    }

    /// <summary>
    /// Parse a Markdown file into a Forkdown document.
    /// </summary>
    /// <param name="markdown">Markdown text to parse.</param>
    /// <param name="fileName">File name to set <see cref="Document.FileName"/> to.</param>
    public static Document From(String markdown, String fileName = "") {
      MarkdownDocument mDoc = BuildMarkdown.From(markdown);
      var result = (Document) BuildForkdown.From(mDoc);
      result.FileName = fileName;
      return result;
    }

    /// <summary>
    /// Convert a parsed Markdown object into the corresponding Forkdown element. 
    /// </summary>
    /// <param name="mdo"></param>
    /// <returns></returns>
    public static Element From(MarkdownObject mdo) {
      Element result = mdo switch
      {
        MarkdownDocument d => new Document(d),
        HeadingBlock h => new Heading(h),
        ListBlock l => new Listing(l),
        ListItemBlock li => new ListItem(li),
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

      result.Subs = subs.Select(BuildForkdown.From).ToList();

      return result;
    }
  }
}
