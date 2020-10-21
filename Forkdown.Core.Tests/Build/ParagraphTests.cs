using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build.Basics {
  public class ParagraphTests {
    [Fact]
    void LinesBecomeParagraphs() {
      const String input = @"First line.
Second line.";
      var result = new MainBuilder().AddWorker<LinesToParagraphsWorker>().Build(input);
      var paras = result.Subs;
      paras.Should().AllBeOfType<Paragraph>().And.HaveCount(2);
      paras[0].FirstSub<Text>().Content.Should().Be("First line.");
      paras[1].FirstSub<Text>().Content.Should().Be("Second line.");
    }
  }
}
