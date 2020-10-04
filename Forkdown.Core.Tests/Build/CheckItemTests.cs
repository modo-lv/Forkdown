using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build {
  public class CheckItemTests {
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    void CheckItem(Boolean vertical) {
      var input = vertical ? "- CheckItem" : "+ CheckItem";
      var result = new MainBuilder().AddWorker<SettingsWorker>().AddWorker<CheckItemWorker>().Build(input);
      result.FirstSub<ExplicitInlineContainer>().IsCheckItem.Should().BeTrue();
    }
  }
}
