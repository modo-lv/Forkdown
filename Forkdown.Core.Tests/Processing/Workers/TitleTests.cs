using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Processing.Workers {
  public class TitleTests {
    [Fact]
    void ListItemTitle() {
      const String input = @"
* **[Wicked](link)**
  Continuation

  Another paragraph
";
      var result = MainBuilder.CreateDefault().Build(input);
      result.Subs[0].Title.Should().Be("Wicked");
    }
    
    [Fact]
    void HeadingWithLinkTitle() {
      const String input = @"# Heading with [link](link)";
      var result = MainBuilder.CreateDefault().Build(input);
      result.Subs[0].Title.Should().Be("Heading with link");
    }
  }
}
