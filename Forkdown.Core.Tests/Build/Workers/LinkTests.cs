using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Config;
using Forkdown.Core.Elements;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build.Workers {
  public class LinkTests {
    [Fact]
    void NoFalseExternalLinks() {
      const String input = @"[Link] {#link}";
      var config = new BuildConfig { ExternalLinks = new ExternalLinkConfig { DefaultUrl = "test://" } };
      var result = new MainBuilder { Config = config }
        .AddWorker<GlobalIdWorker>()
        .AddWorker<LinkIndexWorker>()
        .AddWorker<LinkWorker>()
        .Build(input);
      result.FirstSub<Link>().Target.Should().Be("Link");
    }

    [Fact]
    void ExternalLinksWithIndex() {
      const String input = @"[Link]";
      var config = new BuildConfig { ExternalLinks = new ExternalLinkConfig { DefaultUrl = "test://" } };
      var result = new MainBuilder { Config = config }
        .AddWorker<LinkWorker>()
        .Build(input);
      result.FirstSub<Link>().Target.Should().Be("test://Link");
    }

    [Fact]
    void ExplicitExternalTitle() {
      const String input = @"# [External](@)";
      var result = MainBuilder.CreateDefault().Build(input)
        .FirstSub<Article>()
        .Subs[0] // Header
        .Subs[0] // Heading
        .Subs[0].As<Link>(); // Link

      result.Target.Should().Be("@External");
      result.IsExternal.Should().Be(true);
    }
  }
}
