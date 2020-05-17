using System;
using System.Linq;
using FluentAssertions;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Xunit;
// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.MarkdownParse {
  public class ShorthandLinkTests {
    [Fact]
    void BasicCase() {
      const String input = @"[x]";
      MarkdownDocument result = Main.Parsing.MarkdownParse.ToDocument(input);
      var link = result.First().As<ParagraphBlock>().Inline.First();
      link.Should().BeOfType<LinkInline>();
      link.As<LinkInline>().Url.Should().Be("x");
    }

    [Fact]
    void PlainText() {
      const String input = @"\[x]";
      MarkdownDocument result = Main.Parsing.MarkdownParse.ToDocument(input);
      Inline link = result.First().As<ParagraphBlock>().Inline.First();
      link.Should().NotBeOfType<LinkInline>();
    }

    [Fact]
    void ExplicitLink() {
      const String input = @"[x](y)";
      MarkdownDocument result = Main.Parsing.MarkdownParse.ToDocument(input);
      Inline link = result.First().As<ParagraphBlock>().Inline.First();
      link.Should().BeOfType<LinkInline>();
      link.As<LinkInline>().Url.Should().Be("y");
    }

    [Fact]
    void ReferenceLink() {
      const String input = @"[x]

[x]: y";
      MarkdownDocument result = Main.Parsing.MarkdownParse.ToDocument(input);
      Inline link = result.First().As<ParagraphBlock>().Inline.First();
      link.Should().BeOfType<LinkInline>();
      link.As<LinkInline>().Url.Should().Be("y");
    }
  }
}