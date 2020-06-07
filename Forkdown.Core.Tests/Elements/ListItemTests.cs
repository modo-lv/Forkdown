using System;
using FluentAssertions;
using Forkdown.Core.Elements;
using Forkdown.Core.Parsing.Forkdown;
using Markdig.Renderers.Html;
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
      var doc = ForkdownBuilder.Default.Build(input);
      var li = doc.FirstSub<ListItem>();
      li.HtmlAttributes.Id.Should().Be("id");
      li.HtmlAttributes.Classes.Should().Contain("class");
      li.HtmlAttributes.Properties.ToDictionary().GetOr("attribute", default).Should().Be("value");
      li.Settings.IsTrue("setting").Should().BeTrue();
    }
  }
}
