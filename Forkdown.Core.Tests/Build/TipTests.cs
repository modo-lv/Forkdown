using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build; 

public class TipTests {
  private readonly ForkdownBuild _build = new ForkdownBuild().AddWorker<TipWorker>();

  [Fact]
  void NestedItemLabels() {
    const String input = @"* List
  + `p` Paragraph";
    var result = _build.Run(input);
    result.FirstSub<Item>().Labels.First().Title.Should().Be("p");
  }
    
  [Fact]
  void Multiple() {
    const String input = @"+ `a, b c` Text";
    var result = _build.Run(input);

    var item = result.FirstSub<Item>();
    item.Labels.Select(_ => _.Title).Should().Contain(new List<String> { "a", "b", "c" });
    item.FirstSub<Text>().Content.Should().Be("Text");
    item.FirstSub<Text>().TitleText.Should().Be("Text");
  }
}