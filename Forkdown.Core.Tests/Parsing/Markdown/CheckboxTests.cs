using System;
using FluentAssertions;
using Forkdown.Core.Parsing;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Simpler.NetCore.Collections;
using Xunit;

namespace Forkdown.Core.Tests.Parsing.Markdown {
  public class CheckboxTests {
    [Fact]
    void CheckList() {
      const String input = @"{.:class :one :other=false}
* Test";
      var result = MarkdownBuilder.DefaultBuild(input);
      result[0].As<ListBlock>().GetAttributes().Classes.Should().Contain(":class");
      var props = result[0].As<ListBlock>().GetAttributes().Properties.ToDictionary();
      props[":one"].Should().Be(null);
      props[":other"].Should().Be("false");
    }
    
    [Fact]
    void Parse() {
      const String input = @"x Item";
      var result = MarkdownBuilder.DefaultBuild(input);
      result[0].Should().BeOfType<ListBlock>();
      result[0].As<ListBlock>()[0].Should().BeOfType<ListItemBlock>();
    }
  }
}
