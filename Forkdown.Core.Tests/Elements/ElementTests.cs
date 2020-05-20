using System;
using FluentAssertions;
using Forkdown.Core.Parsing;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Elements {
  public class ElementTests {
    [Fact]
    void HeadingWithLinkTitle() {
      const String input = @"# [Heading](link)";
      var result = BuildForkdown.From(input);
      result.Subs[0].Title.Should().Be("Heading");
    }
  }
}
