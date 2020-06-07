using System;
using FluentAssertions;
using Forkdown.Core.Parsing.Forkdown;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Parsing.Forkdown {
  public class DocumentAttributesTest {
    [Fact]
    void Works() {
      throw new Exception("FIXME");
      /*const String input = @":{#id .class attribute=value}";
      var result = ForkdownBuilder.Default.Build(input);
      result.HtmlAttributes.Id.Should().Be("id");
      result.HtmlAttributes.Classes.Should().Contain("class");
      result.HtmlAttributes.Properties.Get("attribute").Should().Be("value");
      result.Subs.Should().BeEmpty();*/
    }
  }
}
