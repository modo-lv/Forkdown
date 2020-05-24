using System;
using FluentAssertions;
using Forkdown.Core.Elements;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Elements {
  public class ElementTests {
    [Fact]
    void HeadingWithLinkTitle() {
      const String input = @"# Heading with [link](link)";
      var result = Document.From(input);
      result.Subs[0].Title.Should().Be("Heading with link");
    }

    [Fact]
    void ListItemTitle() {
      const String input = @"
* **[Wicked](link)**
  Continuation

  Another paragraph
";
      var result = Document.From(input);
      result.Subs[0].Title.Should().Be("Wicked");
    }
  }
}
