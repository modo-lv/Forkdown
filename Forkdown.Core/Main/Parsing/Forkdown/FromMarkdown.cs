using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements;
using Markdig.Extensions.CustomContainers;
using Tables = Markdig.Extensions.Tables;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Simpler.NetCore.Collections;
using CodeBlock = Forkdown.Core.Elements.CodeBlock;

namespace Forkdown.Core.Parsing.Forkdown {
  /// <summary>
  /// Handles conversion from Markdown to Forkdown elements.
  /// </summary>
  public static class FromMarkdown {
    /// <summary>
    /// Convert a Markdown element to a Forkdown one.
    /// </summary>
    /// <param name="mdo">Markdown object to convert.</param>
    public static Element ToForkdown(IMarkdownObject mdo) {
      Element result = mdo switch {
        MarkdownDocument _ => new Document(),
        HeadingBlock h => new Heading(h),
        ListBlock l => new Listing(l),
        ListItemBlock _ => new ListItem(),
        ParagraphBlock _ => new Paragraph(),
        EmphasisInline e => new Emphasis(e),
        CodeInline c => new Code(c),
        FencedCodeBlock c => new CodeBlock(c),
        LinkInline l => new Link(l),
        LiteralInline t => new Text(t),
        LineBreakInline _ => new LineBreak(),
        ThematicBreakBlock _ => new Separator(),
        CustomContainer c => new ExplicitContainer(c),
        Tables.Table _ => new Table(),
        Tables.TableRow tr => new TableRow(tr),
        Tables.TableCell _ => new TableCell(),
        _ => new Placeholder(mdo),
      };

      var subs = mdo switch {
        LeafBlock b => b.Inline,
        IEnumerable<MarkdownObject> e => e,
        _ => null
      } ?? Nil.E<MarkdownObject>();

      result.Subs = subs.Select(FromMarkdown.ToForkdown).ToList();

      return result;
    }
  }
}
