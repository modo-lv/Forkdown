using System;
using System.Linq;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build {
  public class ItemTests {
    [Fact]
    void InSection() {
      const String input = @":::
+ Test
:::";
      var result = new MainBuilder().AddWorker<ItemWorker>().Build(input);
      result.FirstSub<Item>().IsCheckitem.Should().BeTrue();
    }
    
    [Fact]
    void ItemId() {
      const String input = @"+ Whatever something";
      var result = new MainBuilder().AddWorker<ImplicitIdWorker>().AddWorker<ItemWorker>().Build(input);
      result.FirstSub<Item>().GlobalId.Should().Be($"Whatever{ImplicitIdWorker.W}something");
    }
    
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    void CheckItem(Boolean vertical) {
      var input = vertical ? "+ CheckItem" : "- CheckItem";
      var result = new MainBuilder().AddWorker<ItemWorker>().Build(input);
      result.FirstSub<Item>().TitleText.Should().Be("CheckItem");
      result.FirstSub<Item>().IsNewline.Should().Be(vertical);
    }

    [Fact]
    void FullCheckItem() {
      const String input = @"+ Heading
  Content";
      var result = new MainBuilder()
        .AddWorker<LinesToParagraphsWorker>()
        .AddWorker<SemanticParagraphWorker>()
        .AddWorker<ItemWorker>()
        .Build(input)
        .FirstSub<Item>();
      result.Title.As<Paragraph>().TitleText.Should().Be("Heading");
      result.Content.First().As<Paragraph>().TitleText.Should().Be("Content");
    }
  }
}
