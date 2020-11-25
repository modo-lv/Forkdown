using System;
using FluentAssertions;
using Forkdown.Core.Config;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Config {
  public class RewriteConfigTests {
    [Fact]
    void Load() {
      const String input = @"
external_links:
  rewrites:
    - pattern: X
      rewrite: Y
    - [ 'A', 'B' ]
";

      var result = MainConfig.FromYaml(input).ExternalLinks;

      result.Rewrites[0].Key.IsMatch("^X$").Should().BeTrue();
      result.Rewrites[0].Value.Should().Be("Y");
      result.Rewrites[1].Key.IsMatch("^A$").Should().BeTrue();
      result.Rewrites[1].Value.Should().Be("B");
    }
  }
}
