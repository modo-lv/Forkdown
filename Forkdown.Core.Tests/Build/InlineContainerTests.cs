using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Elements;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build; 

public class InlineContainerTests {
  [Fact]
  void InlineContainer() {
    const String input = @"::Content::";
    var result = new ForkdownBuild().Run(input);
    result.FirstSub<ExplicitInlineContainer>().TitleText.Should().Be("Content");
  }
}