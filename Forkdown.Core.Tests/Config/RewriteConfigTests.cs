using System;
using System.Collections.Generic;
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

      result[0].Should().Be(new KeyValuePair<String, String>("X", "Y"));
      result[1].Should().Be(new KeyValuePair<String, String>("A", "B"));
    }
  }
}
