using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Forkdown.Core.Elements.Markdown;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build; 

public class LinkIndexTests {
  private readonly ForkdownBuild _build = new ForkdownBuild()
    .AddWorker<LinkIndexWorker>();

  private LinkIndex _index => (LinkIndex)_build.Storage.For<LinkIndexWorker>();

  [Fact]
  void MultipleDocuments() {
    _build.Run(@"Text {#text}");
    _build.Run(@"[Link] {#link}");
    _index.Should().ContainKeys("text", "link");
  }

  [Fact]
  void ExplicitIdImplicitClass() {
    const String input = @"[Class] {#id,:id}";
    var doc = _build.Run(input);
    _build.Run("");
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
    var doc = _build.Run(input);
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
    var doc = _build.Run(input);
    doc.FirstSub<Heading>().ExplicitId.Should().Be("h-anchor");
    doc.FirstSub<Paragraph>()!.ExplicitId.Should().Be("panchor");
    var anchors = _index;

    anchors.Count.Should().Be(2);
    anchors["h-anchor"].Should().Be(doc);
    anchors["panchor"].Should().Be(doc);
  }

}