using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Elements;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build {
  public class LabelTests {
    [Fact]
    void Multiple() {
      const String input = @"`a, b c` Text";
      var result = MainBuilder.CreateDefault().Build(input);

      var para = result.FirstSub<Paragraph>();
      para.Labels.Should().BeEquivalentTo("a", "b", "c");
      para.FirstSub<Text>().Content.Should().Be("Text");
      para.FirstSub<Text>().Title.Should().Be("Text");
    }
  }
}
