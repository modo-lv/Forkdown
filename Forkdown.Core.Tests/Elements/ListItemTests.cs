using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Elements {
  public class ListItemTests {
    /// <summary>
    /// The first paragraph of a list item is synonymous with the list item itself, so any attributes assigned to the
    /// first paragraph should be moved to the list item.
    /// </summary>
    [Fact]
    void TakeFirstParagraphAttributes() {
      const String input = @"* Test {#id .class attribute=value :setting}";
      var doc = new MainBuilder().AddWorker<SettingsWorker>().AddWorker<ListItemWorker>().Build(input);
      var li = doc.FirstSub<ListItem>();
      li.Attributes.Id.Should().Be("id");
      li.Attributes.Classes.Should().Contain("class");
      li.Attributes.Properties.ToDictionary().GetOr("attribute", default).Should().Be("value");
      li.Settings.IsTrue("setting").Should().BeTrue();
    }
  }
}
