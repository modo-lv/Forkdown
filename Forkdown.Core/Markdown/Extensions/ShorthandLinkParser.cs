using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace Forkdown.Core.Markdown.Extensions;

/// <summary>
/// Markdown parser for processing Forkdown-style shorthand links.
/// </summary>
/// <example><c>Text [Link] text</c></example>
public class ShorthandLinkParser : InlineParser {
  public ShorthandLinkParser() { this.OpeningCharacters = new[] { '[' }; }

  public override Boolean Match(InlineProcessor processor, ref StringSlice slice) {
    var startPosition = processor.GetSourcePosition(slice.Start, out var line, out var column);
    var last = slice.PeekCharExtra(-1);
    var current = slice.CurrentChar;

    // Backslash escape
    if (last == '\\')
      return false;

    // Link reference
    // Parsing the label moves slice.CurrentChar to just after the end of the link label. 
    if (LinkHelper.TryParseLabel(ref slice, out var label, out var labelSpan))
      if (processor.Document.ContainsLinkReferenceDefinition(label))
        return false;

    // Reference or explicit URL
    if (slice.CurrentChar is '[' or '(')
      return false;

    // Shorthand
    processor.Inline = new LinkInline(label!, "") {
      LabelSpan = processor.GetSourcePositionFromLocalSpan(labelSpan),
      IsImage = false,
      Span = new SourceSpan(
        startPosition,
        processor.GetSourcePosition(slice.Start - 1)),
      Line = line,
      Column = column,
      IsClosed = true,
    }.AppendChild(new LiteralInline(label!));

    return true;
  }
}