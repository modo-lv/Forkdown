using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build; 

public class AttributesTests {
  [Fact]
  void Settings() {
    const String input = @"[xxx]{:setting}";
    var result = new ForkdownBuild().AddWorker<LinesToParagraphsWorker>().Run(input);
    result.FirstSub<Link>().Settings.IsTrue("setting").Should().BeTrue();
  }
}