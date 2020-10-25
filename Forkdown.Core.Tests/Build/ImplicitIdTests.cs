using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build {
  public class ImplicitIdTests {
    private static MainBuilder _builder => new MainBuilder()
      .AddWorker<ExplicitIdWorker>()
      .AddWorker<HeadingItemWorker>()
      .AddWorker<ListItemWorker>()
      .AddWorker<ImplicitIdWorker>();

    [Fact]
    void NestedWithHeading() {
      const String input = @"+ # Heading
  + One
  + Other";
      var result = _builder.Build(input);
      result.FirstSub<ListItem>()
        .FirstSub<ListItem>()
        .GlobalId.Should().Be($"Heading{ImplicitIdWorker.G}One");
    }

    [Fact]
    void TrimSpaces() {
      const String input = @"* Heading {something}";
      var result = _builder.Build(input);
      result.FirstSub<ListItem>().ImplicitId.Should().Be("Heading");
    }

    [Fact]
    void ExplicitId() {
      const String input = @"
# Heading One
* Item
  * X {#xx}
    * Sub
";

      var result = _builder.Build(input);
      result
        .FirstSub<ListItem>()
        .FirstSub<ListItem>()
        .FirstSub<ListItem>().ImplicitId.Should().Be($"xx{ImplicitIdWorker.G}Sub");
    }


    [Fact]
    void ComplicatedExplicitId() {
      const String input = @"# Altar Cave {#altar_cave}
Paragraph.
## Enemies {#enemies}
### Level 1
* [Carbuncle]()";

      var result = _builder.Build(input);
      result
        .FirstSub<Item>()
        .FirstSub<Item>()
        .FirstSub<Item>()
        .FirstSub<ListItem>().ImplicitId.Should().Be(
          $"enemies" +
          $"{ImplicitIdWorker.G}Level{ImplicitIdWorker.W}1" +
          $"{ImplicitIdWorker.G}Carbuncle"
        );
    }

    [Fact]
    void ComplicatedImplicitId() {
      const String input = @"
# The [Floating Continent]()
Paragraph.
## Mainland
### Northeast
Paragraph
* [Killer Bee]
";

      var result = _builder.Build(input);
      result
        .FirstSub<ListItem>().ImplicitId.Should().Be(
          $"The{ImplicitIdWorker.W}Floating{ImplicitIdWorker.W}Continent" +
          $"{ImplicitIdWorker.G}Mainland" +
          $"{ImplicitIdWorker.G}Northeast" +
          $"{ImplicitIdWorker.G}Killer{ImplicitIdWorker.W}Bee"
        );
    }

    [Fact]
    void Duplicates() {
      const String input = @"# Heading One
* Item
* Item
* Item";
      _builder.Build(input)
        .FirstSub<Listing>()
        .Subs[2] // 3rd item
        .GlobalId.Should().Be($"Heading{ImplicitIdWorker.W}One{ImplicitIdWorker.G}Item{ImplicitIdWorker.R}3");
    }

    [Fact]
    void BasicId() {
      const String input = @"# Heading One
* Item one
  * Item two";
      var doc = _builder.Build(input);
      doc
        .FirstSub<ListItem>()
        .FirstSub<ListItem>().GlobalId.Should().Be("Heading⸱One␝Item⸱one␝Item⸱two");
    }
  }
}
