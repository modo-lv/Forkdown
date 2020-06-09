using System;
using FluentAssertions;
using Forkdown.Core.Elements;
using Forkdown.Core.Fd;
using Simpler.NetCore.Collections;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Elements {
  public class ListItemTests {
    /// <summary>
    /// The first paragraph of a list item is synonymous with the list item itself, so any attributes assigned to the
    /// first paragraph should be copied to the list item element as well.
    /// </summary>
    [Fact]
    void TakeFirstParagraphAttributes() {
      const String input = @"* Test {#id .class attribute=value :setting}";
      var doc = FdBuilder.Default.Build(input);
      var li = doc.FirstSub<ListItem>();
      li.Attributes.Id.Should().Be("id");
      li.Attributes.Classes.Should().Contain("class");
      li.Attributes.Properties.ToDictionary().GetOr("attribute", default).Should().Be("value");
      li.Settings.IsTrue("setting").Should().BeTrue();
    }
  }
}
