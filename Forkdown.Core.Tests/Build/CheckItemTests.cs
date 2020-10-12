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
    [Fact]
    void CheckItemId() {
      const String input = @"+ Whatever something";
      var result = new MainBuilder().AddWorker<ImplicitIdWorker>().AddWorker<CheckItemWorker>().Build(input);
      result.FirstSub<CheckItem>().GlobalId.Should().Be($"Whatever{ImplicitIdWorker.W}something");
    }
    
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    void CheckItem(Boolean vertical) {
      var input = vertical ? "+ CheckItem" : "- CheckItem";
      var result = new MainBuilder().AddWorker<CheckItemWorker>().Build(input);
      result.FirstSub<CheckItem>().Title.Should().Be("CheckItem");
      result.FirstSub<CheckItem>().IsNewline.Should().Be(vertical);
    }

    [Fact]
    void FullCheckItem() {
      const String input = @"+ Heading
  Content";
      var result = new MainBuilder()
        .AddWorker<LinesToParagraphsWorker>()
        .AddWorker<SemanticParagraphWorker>()
        .AddWorker<CheckItemWorker>()
        .Build(input)
        .FirstSub<CheckItem>();
      result.Heading.As<Paragraph>().Title.Should().Be("Heading");
      result.Content.First().As<Paragraph>().Title.Should().Be("Content");
    }
  }
}
