using System.Linq;
using Forkdown.Core.Main.Parsing;
using Xunit;
using FluentAssertions;
using Forkdown.Core.Main.Elements;

namespace Forkdown.Core.Tests {
  public class ElementTitleTests {
    [Fact]
    public void WithoutLineBreak() {
      var markdown = @"
# Heading
with line break
";
      var result = ForkdownConvert.ToDocument(markdown);
      result
        .Subs.First().As<Heading>()
        .Title.Should().Be("Heading");
    }
  }
}