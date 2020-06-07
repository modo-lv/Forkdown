using System;
using System.Linq;
using FluentAssertions;
using Forkdown.Core.Elements;
using Forkdown.Core.Parsing.Forkdown;
using Forkdown.Core.Parsing.Forkdown.Processors;
using Markdig.Renderers.Html;
using Simpler.NetCore.Collections;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Parsing.Forkdown {
  public class AttributesTests {
    private static ForkdownBuilder Builder => new ForkdownBuilder()
      .AddProcessor<SettingsProcessor>()
      .AddProcessor<DocumentProcessor>();

    [Fact]
    void Settings() {
      const String input = @"[xxx]{:setting}";
      var result = Builder.Build(input);
      result.FirstSub<Link>().Settings.IsTrue("setting").Should().BeTrue();
    }
  }
}
