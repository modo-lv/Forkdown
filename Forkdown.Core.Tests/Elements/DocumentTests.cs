using System;
using FluentAssertions;
using Forkdown.Core.Parsing.Forkdown;
using Simpler.NetCore.Collections;
using Xunit;
// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Elements {
  public class DocumentTests {
    [Fact]
    void DocumentAttributes() {
      const String input = @":{#id .class attribute=value :setting}";
      var result = ForkdownBuilder.Default.Build(input);
      result.HtmlAttributes.Id.Should().Be("id");
      result.HtmlAttributes.Classes.Should().Contain("class");
      result.HtmlAttributes.Properties.ToDictionary().GetOr("attribute", default).Should().Be("value");
      result.Settings.IsTrue("setting").Should().BeTrue();
      result.Subs.Should().BeEmpty();
    }
  }
}
