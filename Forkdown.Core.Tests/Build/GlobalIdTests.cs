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
        MainBuilder.CreateDefault().Build(input)
      ).Should().Throw<DuplicateAnchorException>();
    }

    [Fact]
    void HandleMultiple() {
      const String input = @"# Heading {#id,heading}";

      var doc = MainBuilder.CreateDefault().Build(input);
      doc.FirstSub<Heading>().ExplicitId.Should().Be("id");
      doc.FirstSub<Heading>().ExplicitIds.Should().Contain("heading");
    }
  }
}
