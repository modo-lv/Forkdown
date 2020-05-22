using System;
using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Parsing;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Elements {
  /// <summary>
  /// Main class for converting Markdown to Forkdown.
  /// </summary>
  public partial class Document {

    /// <summary>
    /// Parse a Markdown file into a Forkdown document.
    /// </summary>
    /// <param name="markdown">Markdown text to parse.</param>
    public static Document From(String markdown) {
      MarkdownDocument mDoc = BuildMarkdown.From(markdown);
      return (Document) from(mDoc);

      static Element from(MarkdownObject mdo) {
        Element result = mdo switch {
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

        var subs = mdo switch {
          LeafBlock b => b.Inline,
          IEnumerable<MarkdownObject> e => e,
          _ => null
        } ?? Nil.E<MarkdownObject>();

        result.Subs = subs.Select(from).ToList();

        if (result is ListItem item && item.Subs[0] is Paragraph par) {
          item.Attributes = par.Attributes;
          par.Attributes = new ElementAttributes();
        }

        return result;
      }
    }
  }
}
