using System;
using FluentAssertions;
using Forkdown.Core.Elements;
using Forkdown.Core.Parsing.Forkdown;
using Xunit;
// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Parsing.Forkdown {
  public class ListItemTests {
    /// <summary>
    /// The first paragraph of a list item is synonymous with the list item itself, so any attributes assigned to the
    /// first paragraph should be copied to the list item element as well.
    /// </summary>
    [Fact]
    void TakeFirstParagraphAttributes() {
      throw new Exception("FIXME");

      /*
      const String input = @"* Test {#id .class property=value}";
      var doc = ForkdownBuilder.Default.Build(input);
      var li = doc.Subs[0].Subs[0].As<ListItem>().HtmlAttributes;
      li.Id.Should().Be("id");
      li.Classes.Should().Contain("class");
      li.Properties["property"].Should().Be("value");*/
    }
  }
}
