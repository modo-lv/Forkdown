using System;
using FluentAssertions;
using Forkdown.Core.Elements;
using Forkdown.Core.Parsing.Forkdown;
using Xunit;
// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Parsing.Forkdown {
  public class TitleTests {
    [Fact]
    void ListItemTitle() {
      const String input = @"
* **[Wicked](link)**
  Continuation

  Another paragraph
";
      var result = ForkdownBuilder.Default.Build(input);
      result.Subs[0].Title.Should().Be("Wicked");
    }
    
    [Fact]
    void HeadingWithLinkTitle() {
      const String input = @"# Heading with [link](link)";
      var result = ForkdownBuilder.Default.Build(input);
      result.Subs[0].Title.Should().Be("Heading with link");
    }
  }
}
