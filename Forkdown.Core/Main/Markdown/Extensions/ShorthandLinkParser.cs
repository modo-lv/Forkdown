using System;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Markdown.Extensions {
  public class ShorthandLinkParser : InlineParser {
    public ShorthandLinkParser() { this.OpeningCharacters = new[] { '[' }; }

    public override Boolean Match(InlineProcessor processor, ref StringSlice slice) {
      var startPosition = processor.GetSourcePosition(slice.Start, out int line, out int column);
      var last = slice.PeekCharExtra(-1);
      var current = slice.CurrentChar;

      if (current != '[' || last == '\\')
        return false;

      // Link reference
      if (LinkHelper.TryParseLabel(ref slice, out String label, out SourceSpan labelSpan))
        if (processor.Document.ContainsLinkReferenceDefinition(label))
          return false;

      // Explicit link
      if (slice.CurrentChar.IsOneOf('[', '(', ':'))
        return false;

      // Shorthand
      processor.Inline = new LinkInline(label, "")
      {
        LabelSpan = processor.GetSourcePositionFromLocalSpan(labelSpan),
        IsImage = false,
        Span = new SourceSpan(
          startPosition,
          processor.GetSourcePosition(slice.Start - 1)),
        Line = line,
        Column = column,
        IsClosed = true,
      }.AppendChild(new LiteralInline(label));

      return true;
    }
  }
}