using System;
using FluentAssertions;
using Forkdown.Core.Elements;
using Forkdown.Core.Parsing.Forkdown;
using Forkdown.Core.Parsing.Forkdown.Processors;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Parsing.Forkdown {
  public class ChecklistTests {
    [Fact]
    void ExplicitId() {
      const String input = @"# Heading One
* Item
  * X {##x}
    * Sub";
      var doc = ForkdownBuilder.Default.Build(input);
      doc
        .Subs[0] // Section
        .Subs[1] // List
        .Subs[0] // Item
        .Subs[1] // X-List
        .Subs[0] // X
        .Subs[1] // Sub-List
        .Subs[0] // Sub
        .As<ListItem>().CheckboxId.Should().Be($"x{ChecklistProcessor.G}Sub");
    }

    [Fact]
    void Duplicates() {
      const String input = @"# Heading One
* Item
* Item
* Item";
      ForkdownBuilder.Default.Build(input)
        .Subs[0] // Section
        .Subs[1] // List
        .Subs[2] // 3rd item
        .As<ListItem>()
        .CheckboxId.Should().Be($"Heading{ChecklistProcessor.W}One{ChecklistProcessor.G}Item{ChecklistProcessor.R}3");
    }

    [Fact]
    void BasicId() {
      const String input = @"# Heading One
* Item one
  * Item two";
      var doc = ForkdownBuilder.Default.Build(input);
      doc
        .Subs[0] // Section
        .Subs[1] // List
        .Subs[0] // Item one
        .Subs[1] // List 2
        .Subs[0] // Item two
        .As<ListItem>().CheckboxId.Should().Be("Heading⸱One␝Item⸱one␝Item⸱two");
    }
  }
}
