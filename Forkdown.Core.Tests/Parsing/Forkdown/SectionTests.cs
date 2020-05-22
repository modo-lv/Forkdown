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
      doc.Subs[0].Should().BeOfType<Section>();
      doc.Subs[1].Should().BeOfType<Section>();
      doc.Subs[0].Subs[0].Should().BeOfType<Heading>();
      doc.Subs[1].Subs[0].Should().BeOfType<Heading>();
    }

    [Fact]
    void HeadingEnclosesContentInSection() {
      const String input = @"# Heading
Paragraph";
      var doc = ForkdownBuilder.Default.Build(input);

      doc.Subs.Count.Should().Be(1);
      doc.Subs[0].Should().BeOfType<Section>();
      doc.Subs[0].Subs[0].Should().BeOfType<Heading>();
      doc.Subs[0].Subs[1].Should().BeOfType<Paragraph>();
    }
  }
}
