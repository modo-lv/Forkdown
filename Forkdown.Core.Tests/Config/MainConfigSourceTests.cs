using FluentAssertions;
using Forkdown.Core.Config;
using Xunit;
// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Config {
  public class MainConfigSourceTests {
    [Fact]
    void IndexLookup() {
      var input = @"
a:
  b: c
d.e:
  f:
    g.h: i
";

      var result = ConfigSource.From(input);

      result["a.b"].Should().Be("c");
      result["d.e.f.g.h"].Should().Be("i");
    }
  }
}
