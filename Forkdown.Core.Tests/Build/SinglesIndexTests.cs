using System;
using System.Linq;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Simpler.NetCore.Collections;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build {
  public class SinglesIndexTests {
    private readonly MainBuilder _builder = new MainBuilder()
      .AddWorker<SettingsWorker>()
      .AddWorker<ArticleWorker>()
      .AddWorker<ImplicitIdWorker>()
      .AddWorker<SinglesIndexWorker>();


    [Fact]
    void Implicit() {
      const String input1 = @"# Heading {:singles}
* One
* Other";
      const String input2 = @"# Heading 2 {:singles}
* One
* Other";

      _builder.Build(input1);
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
      const String input1 = @"* Bob the Blob {:single}";
      const String input2 = @"* Bob the Blob {:single}";

      _builder.Build(input1);
      _builder.Build(input2);

      var index = _builder.Storage.Get<SinglesIndex>()["bob_the_blob"];
      index.Count.Should().Be(2);
      index.All(_ => _ == "Bob⸱the⸱Blob").Should().BeTrue();
    }
  }
}
