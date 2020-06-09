using System;
using FluentAssertions;
using Forkdown.Core.Elements;
using Forkdown.Core.Fd;
using Xunit;
// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Parsing.Forkdown {
  public class LabelTests {
    [Fact]
    void Multiple() {
      const String input = @"`a, b c` Text";
      var result = FdBuilder.Default.Build(input);

      var para = result.FirstSub<Paragraph>();
      para.Labels.Should().BeEquivalentTo("a", "b", "c");
      para.FirstSub<Text>().Content.Should().Be("Text");
      para.FirstSub<Text>().Title.Should().Be("Text");
    }
  }
}
