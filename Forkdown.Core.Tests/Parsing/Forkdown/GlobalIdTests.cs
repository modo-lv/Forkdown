using System;
using FluentAssertions;
using Forkdown.Core.Internal.Exceptions;
using Forkdown.Core.Parsing.Forkdown;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Parsing.Forkdown {
  public class GlobalIdTests {
    [Fact]
    void ThrowOnDuplicate() {
      const String input = @"# One {##id}
# Two {##id}";
      var doc = ForkdownBuilder.Default.Build(input);

      FluentActions.Invoking(() =>
        InternalLinks.From(doc)
      ).Should().Throw<DuplicateAnchorException>();
    }

    [Fact]
    void IgnoreRegularId() {
      const String input = @"## Normal ID {#id}";
      var doc = ForkdownBuilder.Default.Build(input);
      doc.Subs[0].Subs[0].Attributes.Id.Should().Be("id");
    }

    [Fact]
    void RemoveFromClasses() {
      const String input = @"## Heading {##id .#~}";
      var doc = ForkdownBuilder.Default.Build(input);
      doc.Subs[0].Subs[0].Attributes.Id.Should().Be("id");
      doc.Subs[0].Subs[0].GlobalId.Should().Be("id");
      doc.Subs[0].Subs[0].Attributes.Classes.Should().BeEmpty();
    }
  }
}
