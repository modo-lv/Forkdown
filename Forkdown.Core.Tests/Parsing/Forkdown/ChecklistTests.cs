﻿using System;
using FluentAssertions;
using Forkdown.Core.Elements;
using Forkdown.Core.Parsing.Forkdown;
using Forkdown.Core.Parsing.Forkdown.Processors;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Parsing.Forkdown {
  public class ChecklistTests {
    [Fact]
    void ChecklistOn() {
      const String input = @":{:checklist}
# Heading
* Item";
      var result = ForkdownBuilder.Default.Build(input);

      result
        .Subs[0] // Heading section
        .Subs[1] // List
        .Subs[0] // Item
        .As<ListItem>().IsCheckbox.Should().BeTrue();
    }
    
    
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
    void ComplicatedImplicitId() {
      const String input = @"# The [Floating Continent](@~)
`w` Some time after leaving the floating continent, you will permanently lose access to any kind of sea ship and with it all sea enemies.
## `beast` Mainland
### `>` Northeast
Encompasses [Ur], [Kazus] and [Castle Sasune].
* [Killer Bee]";
      var result = ForkdownBuilder.Default.Build(input);
      result.Find<ListItem>()!.CheckboxId.Should().Be(
        $"The{ChecklistProcessor.W}Floating{ChecklistProcessor.W}Continent" +
        $"{ChecklistProcessor.G}Mainland" +
        $"{ChecklistProcessor.G}Northeast" +
        $"{ChecklistProcessor.G}Killer{ChecklistProcessor.W}Bee"
      );
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
