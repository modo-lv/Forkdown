using System;
using FluentAssertions;
using Forkdown.Core.Elements;
using Forkdown.Core.Internal.Exceptions;
using Forkdown.Core.Parsing.Forkdown;
using Markdig.Renderers.Html;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Parsing.Forkdown {
  public class GlobalIdTests {
    [Fact]
    void ThrowOnDuplicate() {
      const String input = @"Text {#id}

More text {#id}";
      var doc = ForkdownBuilder.Default.Build(input);

      FluentActions.Invoking(() =>
        InternalLinks.From(doc)
      ).Should().Throw<DuplicateAnchorException>();
    }

    [Fact]
    void HandleMultiple() {
      const String input = @"# Heading {#id,heading}";

      var doc = ForkdownBuilder.Default.Build(input);
      doc.FirstSub<Article>().GlobalId.Should().Be("id");
      doc.FirstSub<Article>().GlobalIds.Should().Contain("heading");
    }
  }
}
