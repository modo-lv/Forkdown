using System.Linq;
using Xunit;
using FluentAssertions;
using Forkdown.Core.Elements;

namespace Forkdown.Core.Tests {
  public class ElementTitleTests {
    [Fact]
    public void WithoutLineBreak() {
      var markdown = @"
# Heading
with line break
";
      var result = Parsing.Forkdown.FromMarkdown(markdown);
      result
        .Subs.First().As<Heading>()
        .Title.Should().Be("Heading");
    }
  }
}