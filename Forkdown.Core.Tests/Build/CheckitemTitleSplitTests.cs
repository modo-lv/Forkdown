using System;
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
      var result = new MainBuilder().AddWorker<LineBreakToParagraphWorker>().AddWorker<SettingsWorker>().Build(input);
      result.FirstSub<Paragraph>().Settings.Should().ContainKey("setting");
    }

  }
}
