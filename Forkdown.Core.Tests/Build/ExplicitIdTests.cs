using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Xunit;
// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build; 

public class ExplicitIdTests {
  private readonly ForkdownBuild _build = new ForkdownBuild().AddWorker<ExplicitIdWorker>();

  [Fact]
  void Basic() {
    const String input = @"a {#:id}";
    var result = _build.Run(input);
    result.FirstSub<Paragraph>().ExplicitId.Should().Be("a");
  }

  [Fact]
  void ItemTree() {
    const String input = @"
# Heading One
* Item
  * X {#xx}
";

    var result = _build.Run(input);
    result
      .FirstSub<ListItem>()
      .FirstSub<ListItem>().ExplicitId.Should().Be($"xx");
  }
}