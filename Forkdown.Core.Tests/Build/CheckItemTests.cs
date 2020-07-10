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
    void InlineCheckItem(Boolean isExplicit) {
      var input = isExplicit ? "::CheckItem::{:check}" : "::+ CheckItem::";
      var result = new MainBuilder().AddWorker<SettingsWorker>().AddWorker<CheckItemWorker>().Build(input);
      result.FirstSub<ExplicitInlineContainer>().IsCheckItem.Should().BeTrue();
    }
  }
}
