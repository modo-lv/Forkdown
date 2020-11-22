using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build {
  public class LabelTests {
    private readonly ForkdownBuild _build = new ForkdownBuild().AddWorker<LabelWorker>();

    [Fact]
    void Nested() {
      const String input = @"# Heading
* `p` Paragraph";
      var result = _build.Run(input);
      result.FirstSub<Paragraph>().Labels.First().Name.Should().Be("p");
    }
    
    [Fact]
    void Multiple() {
      const String input = @"`a, b c` Text";
      var result = _build.Run(input);

      var para = result.FirstSub<Paragraph>();
      para.Labels.Select(_ => _.Name).Should().Contain(new List<String> { "a", "b", "c" });
      para.FirstSub<Text>().Content.Should().Be("Text");
      para.FirstSub<Text>().TitleText.Should().Be("Text");
    }

    [Fact]
    void Settings() {
      const String input = @"`:w` Text";
      var result = _build.Run(input);
      result.FirstSub<Paragraph>().Settings.IsTrue("w").Should().BeTrue();
    }
  }
}
