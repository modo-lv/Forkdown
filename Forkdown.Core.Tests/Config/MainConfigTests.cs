using System;
using FluentAssertions;
using Forkdown.Core.Config;
using Xunit;
// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Config {
  public class MainConfigTests {
    [Fact]
    void ReadName() {
      const String input = @"name: Test";
      var result = MainConfig.FromYaml(input);
      result.Name.Should().Be("Test");
    }
  }
}
