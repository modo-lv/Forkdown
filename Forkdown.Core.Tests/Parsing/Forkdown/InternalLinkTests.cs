using System;
using FluentAssertions;
using Forkdown.Core.Parsing.Forkdown;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Parsing.Forkdown {
  public class InternalLinkTests {
    [Fact]
    void Implicit() {
      const String input = @"
## [Class Anchor](xxx) {.#~}
## **ID Anchor** {##~}
";
      var doc = ForkdownBuilder.Default.Build(input);
      var links = InternalLinks.From(doc);
      links.Count.Should().Be(2);
      links["class_anchor"].Should().Be(doc);
      links["id_anchor"].Should().Be(doc);
      doc.Subs[1].Subs[0].Attributes.Id.Should().Be("id_anchor");
    }

    [Fact]
    void Explicit() {
      const String input = @"
# Heading {##h-Anchor}
Paragraph {.#pAnchor}
";
      var doc = ForkdownBuilder.Default.Build(input);
      doc.Subs[0].Subs[0].Attributes.Id.Should().Be("h-anchor");
      doc.Subs[0].Subs[1].Attributes.Id.Should().Be("panchor");
      var anchors = InternalLinks.From(doc);

      anchors.Count.Should().Be(2);
      anchors["h-anchor"].Should().Be(doc);
      anchors["panchor"].Should().Be(doc);
    }

  }
}
