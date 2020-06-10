using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Elements;
using Xunit;

namespace Forkdown.Core.Tests.Build.Workers {
  public class ListingTests {
    [Fact]
    void OrderedContainsUnordered() {
      const String input = @"
1. Complete the requirements:
   * Unlock 80% of [bestiary].
";
      var result = MainBuilder.CreateDefault().Build(input);
      var ordered = result.FirstSub<Listing>();
      ordered.IsOrdered.Should().BeTrue();
      var unordered = ordered.FirstSub<Listing>();
      unordered.IsOrdered.Should().BeFalse();
    }
  }
}
