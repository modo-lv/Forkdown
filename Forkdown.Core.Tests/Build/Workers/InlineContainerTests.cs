using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Elements;
using Xunit;
// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build.Workers {
  public class InlineContainerTests {
    [Fact]
    void InlineContainer() {
      const String input = @"::Content::";
      var result = new MainBuilder().Build(input);
      result.FirstSub<ExplicitInlineContainer>().Title.Should().Be("Content");
    }
  }
}
