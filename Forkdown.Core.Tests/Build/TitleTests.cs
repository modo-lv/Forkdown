using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Elements;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build {
  public class TitleTests {
    [Fact]
    void ListItemTitle() {
      const String input = @"
* **[Wicked](link)**
  Continuation

  Another paragraph
";
      var result = ForkdownBuild.Default.Run(input);
      result.Subs[0].TitleText.Should().Be("Wicked");
    }
    
    [Fact]
    void HeadingWithLinkTitle() {
      const String input = @"# Heading with [link](link)";
      var result = ForkdownBuild.Default.Run(input);
      result.FirstSub<Item>().TitleText.Should().Be("Heading with link");
    }
  }
}
