using System;
using FluentAssertions;
using Forkdown.Core.Elements;
using Forkdown.Core.Parsing.Forkdown;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Parsing.Forkdown {
  public class SectionTests {
    [Fact]
    void TwoHeadings() {
      const String input = @"# Heading
## Sub-heading
# Heading 2";
      var doc = ForkdownBuilder.Default.Build(input);

      doc.Subs.Count.Should().Be(2);
      doc.Subs[0].Should().BeOfType<Article>();
      doc.Subs[1].Should().BeOfType<Article>();
      ((Article) doc.Subs[0]).Title.Should().Be("Heading");
      ((Article) doc.Subs[1]).Title.Should().Be("Heading 2");
    }

    [Fact]
    void HeadingBecomesArticle() {
      const String input = @"# Heading
Paragraph";
      var doc = ForkdownBuilder.Default.Build(input);

      doc.Subs.Count.Should().Be(1);
      var article = (Article) doc.Subs[0];
      article.Title.Should().Be("Heading");
      article.Subs[1].As<Paragraph>().Title.Should().Be("Paragraph");
    }
  }
}
