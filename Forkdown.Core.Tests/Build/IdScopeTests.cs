using System;
using System.Linq;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Xunit;
// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build {
  public class IdScopeTests {
    [Fact]
    void InnerScopes() {
      const String input = @"# Top
## Mid
### Bot";
      var result = new MainBuilder().AddWorker<IdScopeWorker>().Build(input);
      result.FirstSub<IdScope>()
        .FirstSub<IdScope>()
        .FirstSub<IdScope>()
        .Should().NotBeNull();
    }
    
    [Fact]
    void ScopeCreated() {
      const String input = @"# Heading";
      var result = new MainBuilder().AddWorker<IdScopeWorker>().Build(input);
      result.Subs.First().Should().BeOfType<IdScope>();
      result.FirstSub<IdScope>().Subs.First().Should().BeOfType<Heading>();
    }
  }
}
