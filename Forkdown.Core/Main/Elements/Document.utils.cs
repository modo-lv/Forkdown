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
          MarkdownDocument d => new Document(),
          HeadingBlock h => new Heading(h),
          ListBlock l => new Listing(l),
          ListItemBlock li => new ListItem(li),
          ParagraphBlock p => new Paragraph(p),
          EmphasisInline e => new Emphasis(e),
          CodeInline c => new Code(c),
          FencedCodeBlock c => new CodeBlock(c),
          LinkInline l => new Link(l),
          LiteralInline t => new Text(t),
          LineBreakInline lb => new LineBreak(),
          ThematicBreakBlock _ => new Separator(),
          _ => new Placeholder(mdo),
        };

        var subs = mdo switch {
          LeafBlock b => b.Inline,
          IEnumerable<MarkdownObject> e => e,
          _ => null
        } ?? Nil.E<MarkdownObject>();

        result.Subs = subs.Select(from).ToList();

        { // Document attributes
          if (result is Document doc
              && doc.Subs[0] is Paragraph par && par.Subs.Count == 1
              && par.Subs[0] is Text text && text.Content == ":") {
            doc.Attributes = par.Attributes;
            doc.Settings = par.Settings;
            par.Attributes = new ElementAttributes();
            par.Settings = new ElementSettings();
            result.Subs.RemoveAt(0);
          }
        }

        { // List item first paragraph attributes move to list item
          if (result is ListItem item && item.Subs[0] is Paragraph par) {
            item.Attributes = par.Attributes;
            item.Settings = par.Settings;
            par.Attributes = new ElementAttributes();
            par.Settings = new ElementSettings();
          }
        }

        return result;
      }
    }
  }
}
