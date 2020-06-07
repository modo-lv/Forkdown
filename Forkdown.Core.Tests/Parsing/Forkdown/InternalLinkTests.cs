using System;
using FluentAssertions;
using Forkdown.Core.Elements;
using Forkdown.Core.Parsing.Forkdown;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Parsing.Forkdown {
  public class InternalLinkTests {
    [Fact]
    void ExplicitIdImplicitClass() {
      const String input = @"[Class] {#id,:id}";
      var doc = ForkdownBuilder.Default.Build(input);
      var links = InternalLinks.From(doc);
      links.Count.Should().Be(2);
      links["id"].Should().Be(doc);
      links["class"].Should().Be(doc);
      doc.Subs[0].GlobalId.Should().Be("id");
    }
    
    [Fact]
    void Implicit() {
      const String input = @"
## [Class Anchor](xxx) {#:id}
## **ID Anchor** {#:id}
";
      var doc = ForkdownBuilder.Default.Build(input);
      var links = InternalLinks.From(doc);
      links.Count.Should().Be(2);
      links["class_anchor"].Should().Be(doc);
      links["id_anchor"].Should().Be(doc);
    }

    [Fact]
    void Explicit() {
      const String input = @"
# Heading {#h-Anchor}
Paragraph {#pAnchor}
";
      var doc = ForkdownBuilder.Default.Build(input);
      doc.FindSub<Article>()!.GlobalId.Should().Be("h-anchor");
      doc.FindSub<Paragraph>()!.GlobalId.Should().Be("panchor");
      var anchors = InternalLinks.From(doc);

      anchors.Count.Should().Be(2);
      anchors["h-anchor"].Should().Be(doc);
      anchors["panchor"].Should().Be(doc);
    }

  }
}
