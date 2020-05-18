using System;
using System.Linq;
using FluentAssertions;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Xunit;
// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Markdown {
  public class LinkTests {
    [Fact]
    void ExplicitExternalShorthand() {
      const String input = @"[x](@)";

      MarkdownDocument result = Parsing.Markdown.ToDocument(input);
      var link = result.Single().As<ParagraphBlock>().Inline.Single().As<LinkInline>();

      link.Url.Should().Be("@");
    }
  }
}