using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Config;
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
      para.Labels.Select(_ => _.Name).Should().Contain(new List<String> { "a", "b", "c" });
      para.FirstSub<Text>().Content.Should().Be("Text");
      para.FirstSub<Text>().TitleText.Should().Be("Text");
    }

    [Fact]
    void Settings() {
      const String input = @"`:w` Text";
      var result = new MainBuilder().AddWorker<LabelWorker>().Build(input);
      result.FirstSub<Paragraph>().Settings.IsTrue("w").Should().BeTrue();
    }
  }
}
