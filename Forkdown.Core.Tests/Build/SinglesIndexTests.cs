using System;
using System.Linq;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build {
  public class SinglesIndexTests {
    private readonly ForkdownBuild _build = new ForkdownBuild().AddWorker<SinglesIndexWorker>();
    private SinglesIndex _index => (SinglesIndex)_build.Storage.For<SinglesIndexWorker>();

    [Fact]
    void ExplicitId() {
      const String input = @"
+ Item 1 {:single=both}
+ Item 2 {:single=both}";
      _build.Run(input);
      var entry = _index["both"];

      entry.Count.Should().Be(2);
      entry.Should().Contain("Item⸱1");
      entry.Should().Contain("Item⸱2");
    }

    [Fact]
    void WithText() {
      const String input = @"
+ {:single} General
  :w entry unattainable until [next playthrough](NG+).
";
      FromMarkdown.ToForkdown(input);
      _build.Run(input);

      _index.Should().ContainKey("general");
    }


    [Fact]
    void Implicit() {
      const String input1 = @"+ # Heading {:singles}
  + One
  + Other";
      const String input2 = @"+ # Heading 2 {:singles}
  + One
  + Other";

      _build.Run(input1);
      _build.Run(input2);

      var one = _index["one"];
      var two = _index["other"];

      one.Count.Should().Be(2);
      two.Count.Should().Be(2);

      one.Should().Contain(new[] { "Heading␝One", "Heading⸱2␝One" });
      two.Should().Contain(new[] { "Heading␝Other", "Heading⸱2␝Other" });
    }


    [Fact]
    void Basic() {
      const String input1 = @"+ Bob the Blob {:single}";
      const String input2 = @"+ Bob the Blob {:single}";

      _build.Run(input1);
      _build.Run(input2);

      var index = _index["bob_the_blob"];
      index.Count.Should().Be(2);
      index.All(_ => _ == "Bob⸱the⸱Blob").Should().BeTrue();
    }
  }
}
