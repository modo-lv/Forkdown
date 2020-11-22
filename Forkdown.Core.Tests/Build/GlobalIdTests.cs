using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Elements;
using Forkdown.Core.Internal.Exceptions;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build {
  public class GlobalIdTests {
    [Fact]
    void ThrowOnDuplicate() {
      const String input = @"Text {#id}

More text {#id}";
      FluentActions.Invoking(() =>
        ForkdownBuild.Default.Run(input)
      ).Should().Throw<DuplicateAnchorException>();
    }

    [Fact]
    void HandleMultiple() {
      const String input = @"Heading {#id,heading}";

      var doc = ForkdownBuild.Default.Run(input);
      doc.FirstSub<Paragraph>().ExplicitId.Should().Be("id");
      doc.FirstSub<Paragraph>().ExplicitIds.Should().Contain("heading");
    }
  }
}
