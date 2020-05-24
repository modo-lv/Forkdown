using System;
using FluentAssertions;
using Forkdown.Core.Parsing.Forkdown;
using Simpler.Net;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Parsing.Forkdown {
  public class DocumentAttributesTest {
    [Fact]
    void Works() {
      const String input = @":{#id .class attribute=value}";
      var result = ForkdownBuilder.Default.Build(input);
      result.Attributes.Id.Should().Be("id");
      result.Attributes.Classes.Should().Contain("class");
      result.Attributes.Properties.Get("attribute").Should().Be("value");
      result.Subs.Should().BeEmpty();
    }
  }
}
