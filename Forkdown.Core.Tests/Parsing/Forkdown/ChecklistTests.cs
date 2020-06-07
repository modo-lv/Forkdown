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
    void ChecklistOff() {
      const String input = @"# Heading
* List item";
      var result = ForkdownBuilder.Default.Build(input);
      result.FirstSub<Listing>().IsChecklist.Should().BeFalse();
      result.FirstSub<ListItem>().IsCheckbox.Should().BeFalse();
    }
    
    [Fact]
    void ChecklistHeading() {
      const String input = @"# Heading {:checklist}
* Checkbox";
      var result = ForkdownBuilder.Default.Build(input);
      result.FirstSub<Listing>().IsChecklist.Should().BeTrue();
      result.FirstSub<ListItem>().IsCheckbox.Should().BeTrue();
    }


    [Fact]
    void ChecklistOn() {
      const String input = @":{:checklist}
# Heading
* Item";
      var result = ForkdownBuilder.Default.Build(input);

      result.FirstSub<ListItem>().IsCheckbox.Should().BeTrue();
    }
  }
}
