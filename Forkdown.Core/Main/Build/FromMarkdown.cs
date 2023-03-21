using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Markdown;
using Forkdown.Core.Markdown;
using Markdig.Extensions.CustomContainers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Simpler.NetCore.Collections;
using Tables = Markdig.Extensions.Tables;
using CodeBlock = Forkdown.Core.Elements.Markdown.CodeBlock;
using Table = Forkdown.Core.Elements.Table;
using TableCell = Forkdown.Core.Elements.TableCell;
using TableRow = Forkdown.Core.Elements.TableRow;

namespace Forkdown.Core.Build; 

/// <summary>
/// Handles conversion from Markdown to Forkdown elements.
/// </summary>
public static class FromMarkdown {
  public static Document ToForkdown(ProjectPath file) =>
    ToForkdown(File.ReadAllText(file.FullPathString()), file);


  /// <summary>
  /// Parse a Markdown string into a tree of Forkdown elements.
  /// </summary>
  /// <param name="markdown">Markdown to parse.</param>
  /// <param name="file">Project file the markdown is from, if any.</param>
  public static Document ToForkdown(String markdown, ProjectPath? file = null) {
    var mDoc = MarkdownBuilder.DefaultBuild(markdown);
    var fDoc = (Document) ToForkdown(mDoc);
    fDoc.ProjectFilePath = file?.RelPathString() ?? "";
    return fDoc;
  }
    
  /// <summary>
  /// Convert a Markdown element to a Forkdown one.
  /// </summary>
  /// <param name="mdo">Markdown object to convert.</param>
  public static Element ToForkdown(IMarkdownObject mdo) {
    Element result = mdo switch {
      MarkdownDocument _ => new Document(),
      HeadingBlock h => new Heading(h),
      ListBlock l => new Listing(l),
      ListItemBlock li => new ListItem(li),
      ParagraphBlock p => new Paragraph(p),
      CustomContainer c => new Section(c),
      CustomContainerInline c => new ExplicitInlineContainer(c),
      CodeInline c => new Code(c),
      FencedCodeBlock c => new CodeBlock(c),
      LinkInline l => new Link(l),
      LiteralInline t => new Text(t),
      LineBreakInline _ => new LineBreak(),
      ThematicBreakBlock _ => new Separator(),
      Tables.Table t => new Table(t),
      Tables.TableRow tr => new TableRow(tr),
      Tables.TableCell tc => new TableCell(tc),
      EmphasisInline e => new Emphasis(e),
      HtmlBlock b => new Html(b),
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