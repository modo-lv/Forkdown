using System;
using System.Linq;
using FluentAssertions;
using Forkdown.Core.Markdown;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using NUnit.Framework;

namespace Forkdown.Core.Tests.Markdown; 

public class ShorthandLinkTests {
  [Test]
  public void AllowReferences() {
    const String input = @"[x][y]

[y]: z";
    var result = MarkdownBuilder.DefaultBuild(input);
    var link = result.First().As<ParagraphBlock>().Inline!.Single().As<LinkInline>();
    link.Url.Should().Be("z");
  }
  
  [Test]
  public void BasicCase() {
    const String input = @"[x], y and z";
    var result = MarkdownBuilder.DefaultBuild(input);
    var link = result.First().As<ParagraphBlock>().Inline!.First().As<LinkInline>();
    link.Url.Should().Be("x");
    var text = link.Single().As<LiteralInline>();
    text.Content.Text.Should().Be("x");
  }

  [Test]
  public void PlainText() {
    const String input = @"\[x]";
    var result = MarkdownBuilder.DefaultBuild(input);
    var text = result.First().As<ParagraphBlock>().Inline!.First();
    text.Should().NotBeOfType<LinkInline>();
  }

  [Test]
  public void ExplicitLink() {
    const String input = @"[x](y)";
    var result = MarkdownBuilder.DefaultBuild(input);
    var link = result.First().As<ParagraphBlock>().Inline!.First();
    link.Should().BeOfType<LinkInline>();
    link.As<LinkInline>().Url.Should().Be("y");
  }

  [Test]
  public void ReferenceLink() {
    const String input = @"[x]

[x]: y";
    var result = MarkdownBuilder.DefaultBuild(input);
    var link = result.First().As<ParagraphBlock>().Inline!.First();
    link.Should().BeOfType<LinkInline>();
    link.As<LinkInline>().Url.Should().Be("y");
  }
}