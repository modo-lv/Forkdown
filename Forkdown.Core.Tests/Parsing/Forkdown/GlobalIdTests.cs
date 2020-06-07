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
      var input = new Document().AddSubs(
        new Text { Settings = { { "#id", null } } },
        new Text { Settings = { { "#id", null } } }
      );
      var doc = ForkdownBuilder.Default.Build(input);

      FluentActions.Invoking(() =>
        InternalLinks.From(doc)
      ).Should().Throw<DuplicateAnchorException>();
    }

    [Fact]
    void HandleMultiple() {
      var input = new Document().AddSub(
        new Article((Heading)
          new Heading(1) { Settings = { { "#id", null }, { "#~", null } } }.AddSub(new Text("Heading"))
        )
      );

      var doc = ForkdownBuilder.Default.Build(input);
      doc.FirstSub<Article>().GlobalId.Should().Be("id");
      doc.FirstSub<Article>().GlobalIds.Should().Contain("heading");
    }
  }
}
