using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FluentAssertions;
using Forkdown.Core.Config;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Config {
  public class RewriteConfigTests {
    [Fact]
    void Load() {
      const String input = @"
externalLinks:
  rewrites:
    - pattern: X
      rewrite: Y
    - [ 'A', 'B' ]
";

      var result = ExternalLinkRewriteConfig.From(input);

      result[0].Key.IsMatch("^X$").Should().BeTrue();
      result[0].Value.Should().Be("Y");
      result[1].Key.IsMatch("^A$").Should().BeTrue();
      result[1].Value.Should().Be("B");
    }
  }
}
