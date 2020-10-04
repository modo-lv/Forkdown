using System;
using System.Linq;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build {
  public class CheckItemTests {
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    void CheckItem(Boolean vertical) {
      var input = vertical ? "+ CheckItem" : "- CheckItem";
      var result = new MainBuilder().AddWorker<CheckItemWorker>().Build(input);
      var checklist = result.FirstSub<Listing>();
      checklist.IsVertical.Should().Be(vertical);
      checklist.FirstSub<CheckItem>().Title.Should().Be("CheckItem");
    }

    [Fact]
    void FullCheckItem() {
      const String input = @"+ Heading
  :? Help
  Content";
      var result = new MainBuilder()
        .AddWorker<LinesToParagraphsWorker>()
        .AddWorker<SemanticParagraphWorker>()
        .AddWorker<CheckItemWorker>()
        .Build(input)
        .FirstSub<CheckItem>();
      result.Heading.As<Paragraph>().Title.Should().Be("Heading");
      result.Help.As<Paragraph>().Title.Should().Be("Help");
      result.Content.First().As<Paragraph>().Title.Should().Be("Content");
    }
  }
}
