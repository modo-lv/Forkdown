using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Builders;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Simpler.NetCore;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build {
  public class LinkIndexTests {
    private readonly ForkdownBuild _builder = new ForkdownBuild()
      .AddBuilder<LinkIndexBuilder>();

    private LinkIndex _index => _builder.Results[typeof(LinkIndexBuilder)].Value.As<LinkIndex>();

    [Fact]
    void MultipleDocuments() {
      _builder.Build(@"Text {#text}");
      _builder.Build(@"[Link] {#link}");
      _index.Should().ContainKeys("text", "link");
    }

    [Fact]
    void ExplicitIdImplicitClass() {
      const String input = @"[Class] {#id,:id}";
      var doc = _builder.Build(input);
      _builder.Build("");
      var links = _index;
      links.Count.Should().Be(2);
      links["id"].Should().Be(doc);
      links["class"].Should().Be(doc);
      doc.Subs[0].ExplicitId.Should().Be("id");
    }

    [Fact]
    void Implicit() {
      const String input = @"
## [Class Anchor](xxx) {#:id}
## **ID Anchor** {#:id}
";
      var doc = _builder.Build(input);
      var links = _index;
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
      var doc = _builder.Build(input);
      doc.FirstSub<Heading>().ExplicitId.Should().Be("h-anchor");
      doc.FirstSub<Paragraph>()!.ExplicitId.Should().Be("panchor");
      var anchors = _index;

      anchors.Count.Should().Be(2);
      anchors["h-anchor"].Should().Be(doc);
      anchors["panchor"].Should().Be(doc);
    }

  }
}
