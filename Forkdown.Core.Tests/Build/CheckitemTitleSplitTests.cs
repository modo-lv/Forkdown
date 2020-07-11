﻿using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Xunit;

namespace Forkdown.Core.Tests.Build {
  public class CheckitemTitleSplitTests {
    [Fact]
    void SettingsTransfer() {
      const String input = @"
+ {:setting} First line
  Some more lines
";
      var result = new MainBuilder().AddWorker<CheckitemTitleSplitWorker>().AddWorker<SettingsWorker>().Build(input);
      result.FirstSub<Paragraph>().Settings.Should().ContainKey("setting");
    }
    
    [Fact]
    void KeepAllElements() {
      const String input = @"* Item
New line";
      var result = new MainBuilder().AddWorker<CheckitemTitleSplitWorker>().Build(input);
      var li = result.FirstSub<ListItem>();
      li.Subs.Count.Should().Be(2);
      li.Subs[1].As<Paragraph>().IsTitle.Should().BeTrue();
    }
  }
}
