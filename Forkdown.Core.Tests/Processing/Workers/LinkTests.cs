using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Elements;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Processing.Workers {
  public class LinkTests {
    [Fact]
    void ExplicitExternalTitle() {
      const String input = @"# [External](@)";
      var result = MainBuilder.CreateDefault().Build(input)
        .Subs[0]             // Article
        .Subs[0]             // Header
        .Subs[0]             // Heading
        .Subs[0].As<Link>(); // Link

      result.Target.Should().Be("@External");
      result.IsExternal.Should().Be(true);
    }
  }
}
