using System;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Build.Workers;
using Forkdown.Core.Elements;
using Xunit;
// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build; 

public class DocumentAttributesTests {
  [Fact]
  void SimpleCase() {
    const String input = ":{#id .class property :setting}";
    var result = new ForkdownBuild().AddWorker<DocumentAttributesWorker>().Run(input);
    var doc = result.As<Document>();
    doc.ExplicitId.Should().Be("id");
    doc.Attributes.Classes.Should().Contain("class");
    doc.Attributes.Properties.Should().ContainKey("property");
    doc.Settings.IsTrue("setting").Should().BeTrue();
  }
}