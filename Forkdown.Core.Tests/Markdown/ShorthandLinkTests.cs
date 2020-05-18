using System;
using System.Linq;
using FluentAssertions;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Markdown {
  public class ShorthandLinkTests {
    [Fact]
    void BasicCase() {
      const String input = @"[x], y and z";
      MarkdownDocument result = Parsing.Markdown.ToDocument(input);
      var link = result.First().As<ParagraphBlock>().Inline.First().As<LinkInline>();
      link.Url.Should().Be("x");
      var text = link.Single().As<LiteralInline>();
      text.Content.Text.Should().Be("x");
    }

    [Fact]
    void PlainText() {
      const String input = @"\[x]";
      MarkdownDocument result = Parsing.Markdown.ToDocument(input);
      Inline text = result.First().As<ParagraphBlock>().Inline.First();
      text.Should().NotBeOfType<LinkInline>();
    }

    [Fact]
    void ExplicitLink() {
      const String input = @"[x](y)";
      MarkdownDocument result = Parsing.Markdown.ToDocument(input);
      Inline link = result.First().As<ParagraphBlock>().Inline.First();
      link.Should().BeOfType<LinkInline>();
      link.As<LinkInline>().Url.Should().Be("y");
    }

    [Fact]
    void ReferenceLink() {
      const String input = @"[x]

[x]: y";
      MarkdownDocument result = Parsing.Markdown.ToDocument(input);
      Inline link = result.First().As<ParagraphBlock>().Inline.First();
      link.Should().BeOfType<LinkInline>();
      link.As<LinkInline>().Url.Should().Be("y");
    }
  }
}