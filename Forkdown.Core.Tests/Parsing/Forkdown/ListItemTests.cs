using System;
using FluentAssertions;
using Forkdown.Core.Elements;
using Forkdown.Core.Fd;
using Xunit;

namespace Forkdown.Core.Tests.Parsing.Forkdown {
  public class ListItemTests {
    [Fact]
    void BulletChar() {
      const String input = @"+ Plus
- Minus
* Star";
      var result = FdBuilder.Default.Build(input);

      result.Subs[0].FirstSub<ListItem>().BulletChar.Should().Be('+');
      result.Subs[1].FirstSub<ListItem>().BulletChar.Should().Be('-');
      result.Subs[2].FirstSub<ListItem>().BulletChar.Should().Be('*');
    }
  }
}
