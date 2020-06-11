using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Simpler.NetCore.Collections;
using Xunit;
// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Elements {
  public class DocumentTests {
    [Fact]
    void DocumentAttributes() {
      const String input = @":{#id .class attribute=value :setting}";
      var result = new MainBuilder().AddWorker<SettingsWorker>().AddWorker<DocumentWorker>().Build(input);
      result.Attributes.Id.Should().Be("id");
      result.Attributes.Classes.Should().Contain("class");
      result.Attributes.Properties.ToDictionary().GetOr("attribute", default).Should().Be("value");
      result.Settings.IsTrue("setting").Should().BeTrue();
      result.Subs.Should().BeEmpty();
    }
  }
}
