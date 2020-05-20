using System;
using System.Linq;
using FluentAssertions;
using Xunit;
using Forkdown.Core.Parsing;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Parsing {
  public class AnchorTests {
    [Fact]
    void IgnoreRegularId() {
      const String input = @"## Normal ID {#id}";
      var doc = BuildForkdown.From(input);
      var anchors = AnchorIndex.BuildFrom(new[] { doc });

      anchors.Count.Should().Be(0);
      doc.Subs[0].Attributes.Id.Should().Be("id");
    }
    
    [Fact]
    void ImplicitAnchors() {
      const String input = @"
## [Class Anchor](xxx) {.#~}
## **ID Anchor** {##~}
";
      var doc = BuildForkdown.From(input);
      doc.Subs.First().Attributes.Classes.Count.Should().Be(1);

      var anchors = AnchorIndex.BuildFrom(new[] { doc });

      anchors.Count.Should().Be(2);
      anchors["class_anchor"].Should().Be(doc);
      anchors["id_anchor"].Should().Be(doc);
      doc.Subs[1].Attributes.Id.Should().Be("id_anchor");
    }

    [Fact]
    void ExplicitAnchors() {
      const String input = @"
# Heading {##h-Anchor}
Paragraph {.#pAnchor}
";
      var doc = BuildForkdown.From(input);
      doc.Subs[0].Attributes.Id.Should().Be("#h-Anchor");
      doc.Subs[1].Attributes.Classes.FirstOrDefault().Should().Be("#pAnchor");
      var anchors = AnchorIndex.BuildFrom(new[] { doc });

      anchors.Count.Should().Be(2);
      anchors["h-anchor"].Should().Be(doc);
      anchors["panchor"].Should().Be(doc);
    }
  }
}
