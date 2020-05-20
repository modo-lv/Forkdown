using System;
using FluentAssertions;
using Forkdown.Core.Parsing;
using Xunit;

namespace Forkdown.Core.Tests.Elements {
  public class DocumentTests {
    [Fact]
    void ClassAssignment() {
      const String input = @"{.class}";
      var result = BuildForkdown.From(input);
      result.Attributes.Classes.Should().Contain("class");
    }
  }
}
