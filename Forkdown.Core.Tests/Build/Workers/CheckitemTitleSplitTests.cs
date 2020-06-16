using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Xunit;

namespace Forkdown.Core.Tests.Build.Workers {
  public class CheckitemTitleSplitTests {
    [Fact]
    void KeepAllElements() {
      const String input = @"* Item
New line";
      var result = new MainBuilder().AddWorker<CheckitemTitleSplitWorker>().Build(input);
      var li = result.FirstSub<ListItem>();
      li.Subs.Count.Should().Be(2);
    }
  }
}
