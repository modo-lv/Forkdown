using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build {
  public class LinesToPargraphsTests {
    [Fact]
    void SimpleCase() {
      const String input = "a\nb";
      var result = new ForkdownBuild().AddWorker<LinesToParagraphsWorker>().Run(input);
      result.Subs.Should().HaveCount(2);
      result.Subs[0].As<Paragraph>().TitleText.Should().Be("a");
      result.Subs[1].As<Paragraph>().TitleText.Should().Be("b");
    }
  }
}
