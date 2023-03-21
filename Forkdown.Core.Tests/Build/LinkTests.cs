using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Config;
using Forkdown.Core.Elements;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build; 

public class LinkTests {
  [Fact]
  void NoFalseExternalLinks() {
    const String input = @"[Link] {#link}";
    var config = new MainConfig { ExternalLinks = new ExternalLinkConfig { DefaultUrl = "test://" } };
    var result = new ForkdownBuild().WithConfig(config)
      .AddWorker<LinkWorker>()
      .Run(input);
    result.FirstSub<Link>().Target.Should().Be("Link");
  }

  [Fact]
  void ExternalLinksWithIndex() {
    const String input = @"[Link]";
    var config = new MainConfig { ExternalLinks = new ExternalLinkConfig { DefaultUrl = "test://" } };
    var result = new ForkdownBuild().WithConfig(config)
      .AddWorker<LinkWorker>()
      .Run(input);
    result.FirstSub<Link>().Target.Should().Be("test://Link");
  }

  [Fact]
  void ExplicitExternalTitle() {
    const String input = @"# [External](@)";
    var result = ForkdownBuild.Default.Run(input)
      .FirstSub<Link>();

    result.Target.Should().Be("@External");
    result.IsExternal.Should().Be(true);
  }
}