using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Elements;
using Xunit;

namespace Forkdown.Core.Tests.Build.Workers {
  public class ListItemTests {
    [Fact]
    void BulletChar() {
      const String input = @"+ Plus
- Minus
* Star";
      var result = MainBuilder.CreateDefault().Build(input);

      result.Subs[0].FirstSub<ListItem>().BulletChar.Should().Be('+');
      result.Subs[1].FirstSub<ListItem>().BulletChar.Should().Be('-');
      result.Subs[2].FirstSub<ListItem>().BulletChar.Should().Be('*');
    }
  }
}
