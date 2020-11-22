using System;
using System.Linq;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build {
  public class HeadingItemTests {
    [Fact]
    void ItemWithHeading() {
      const String input = @"+ # Heading";
      var result = new ForkdownBuild().AddWorker<HeadingItemWorker>().Run(input);
      result.Subs[0].Subs[0].Should().BeOfType<ListItem>();
      result.FirstSub<ListItem>().Subs[0].Should().BeOfType<Heading>();
    }


    [Fact]
    void InnerItems() {
      const String input = @"# Top
## Mid
### Bot";
      var result = new ForkdownBuild().AddWorker<HeadingItemWorker>().Run(input);
      result.FirstSub<Item>()
        .FirstSub<Item>()
        .FirstSub<Item>()
        .IsHeading.Should().BeTrue();
    }

    [Fact]
    void ItemCreated() {
      const String input = @"# Heading";
      var result = new ForkdownBuild().AddWorker<HeadingItemWorker>().Run(input);
      result.Subs.First().Should().BeOfType<Item>();
      result.FirstSub<Item>().Title.Should().BeOfType<Heading>();
    }
  }
}
