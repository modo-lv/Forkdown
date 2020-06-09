using System;
using FluentAssertions;
using Forkdown.Core.Elements;
using Forkdown.Core.Fd;
using Forkdown.Core.Internal.Exceptions;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Parsing.Forkdown {
  public class GlobalIdTests {
    [Fact]
    void ThrowOnDuplicate() {
      const String input = @"Text {#id}

More text {#id}";
      FluentActions.Invoking(() =>
        FdBuilder.Default.Build(input)
      ).Should().Throw<DuplicateAnchorException>();
    }

    [Fact]
    void HandleMultiple() {
      const String input = @"# Heading {#id,heading}";

      var doc = FdBuilder.Default.Build(input);
      doc.FirstSub<Article>().GlobalId.Should().Be("id");
      doc.FirstSub<Article>().GlobalIds.Should().Contain("heading");
    }
  }
}
