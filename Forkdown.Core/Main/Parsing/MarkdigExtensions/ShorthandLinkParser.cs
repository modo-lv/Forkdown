using System;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Main.Parsing.MarkdigExtensions {
  public class ShorthandLinkParser : InlineParser {
    public ShorthandLinkParser() { this.OpeningCharacters = new[] { '[' }; }

    public override Boolean Match(InlineProcessor processor, ref StringSlice slice) {
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
      processor.Inline = new LinkInline(label, label);
      processor.Inline.Span.End = processor.Inline.Span.Start + label.Length;
      return true;
    }
  }
}