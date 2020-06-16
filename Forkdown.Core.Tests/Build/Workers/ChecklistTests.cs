using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Elements;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build.Workers {
  public class ChecklistTests {
    [Fact]
    void ChecklistOff() {
      const String input = @"# Heading
* List item";
      var result = MainBuilder.CreateDefault().Build(input);
      result.FirstSub<Listing>().IsChecklist.Should().BeFalse();
      result.FirstSub<ListItem>().IsCheckitem.Should().BeFalse();
    }
    
    [Fact]
    void ChecklistHeading() {
      const String input = @"# Heading {:checklist}
* Checkbox";
      var result = MainBuilder.CreateDefault().Build(input);
      result.FirstSub<Listing>().IsChecklist.Should().BeTrue();
      result.FirstSub<ListItem>().IsCheckitem.Should().BeTrue();
    }


    [Fact]
    void ChecklistOn() {
      const String input = @":{:checklist}
# Heading
* Item";
      var result = MainBuilder.CreateDefault().Build(input);

      result.FirstSub<ListItem>().IsCheckitem.Should().BeTrue();
    }
  }
}
