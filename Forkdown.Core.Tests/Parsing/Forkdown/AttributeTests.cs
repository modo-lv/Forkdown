using System;
using FluentAssertions;
using Forkdown.Core.Elements;
using Forkdown.Core.Fd;
using Forkdown.Core.Fd.Processors;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Parsing.Forkdown {
  public class AttributesTests {
    private static FdBuilder Builder => new FdBuilder()
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
