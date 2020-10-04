using System;
using System.Linq;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build {
  public class SinglesIndexTests {
    private readonly MainBuilder _builder = new MainBuilder()
      .AddWorker<ExplicitIdWorker>()
      .AddWorker<SettingsWorker>()
      .AddWorker<ArticleWorker>()
      .AddWorker<ImplicitIdWorker>()
      .AddWorker<ListItemWorker>()
      .AddWorker<CheckItemWorker>()
      .AddWorker<SinglesIndexWorker>();

    [Fact]
    void ExplicitId() {
      const String input = @"
+ Item 1 {:single=both}
+ Item 2 {:single=both}";
      _builder.Build(input);
      var index = _builder.Storage.Get<SinglesIndex>();
      var entry = index["both"];

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
      var builder = MainBuilder.CreateDefault();
      var test = FromMarkdown.ToForkdown(input);
      var result = builder.Build(input);
      var index = builder.Storage.Get<SinglesIndex>();

      index.Should().ContainKey("general");
    }


    [Fact]
    void Implicit() {
      const String input1 = @"# Heading {:singles}
+ One
+ Other";
      const String input2 = @"# Heading 2 {:singles}
+ One
+ Other";

      var result = _builder.Build(input1);
      _builder.Build(input2);

      var index = _builder.Storage.Get<SinglesIndex>();
      var one = index["one"];
      var two = index["other"];

      one.Count.Should().Be(2);
      two.Count.Should().Be(2);

      one.Should().Contain(new[] { "Heading␝One", "Heading⸱2␝One" });
      two.Should().Contain(new[] { "Heading␝Other", "Heading⸱2␝Other" });
    }


    [Fact]
    void Basic() {
      const String input1 = @"+ Bob the Blob {:single}";
      const String input2 = @"+ Bob the Blob {:single}";

      _builder.Build(input1);
      _builder.Build(input2);

      var index = _builder.Storage.Get<SinglesIndex>()["bob_the_blob"];
      index.Count.Should().Be(2);
      index.All(_ => _ == "Bob⸱the⸱Blob").Should().BeTrue();
    }
  }
}
