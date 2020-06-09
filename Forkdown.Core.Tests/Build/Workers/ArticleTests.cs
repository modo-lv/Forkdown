using System;
using System.Collections.Generic;
using FluentAssertions;
using Forkdown.Core.Build;
using Forkdown.Core.Elements;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Forkdown.Core.Tests.Build.Workers {
  public class ArticleTests {
    [Fact]
    void CopyAttributes() {
      const String input = @"# Heading {#id .class property=value}";
      var result = MainBuilder.CreateDefault().Build(input);
      var article = result.FirstSub<Article>();
      article.Attributes.Id.Should().Be("id");
      article.Attributes.Classes.Should().Contain("class");
      article.Attributes.Properties.Should().Contain(new KeyValuePair<String, String>("property", "value"));
    }
    
    [Fact]
    void CopySettings() {
      var input = (Document) new Document().AddSub(
        new Article((Heading)
          new Heading(1) { Settings = { { "setting", null } } }.AddSub(new Text("Heading"))
        )
      );

      var result = MainBuilder.CreateDefault().Build(input);
      result.FirstSub<Article>().Settings.IsTrue("setting").Should().BeTrue();
    }

    [Fact]
    void TwoHeadings() {
      const String input = @"# Heading
## Sub-heading
# Heading 2";
      var doc = MainBuilder.CreateDefault().Build(input);

      doc.Subs.Count.Should().Be(2);
      doc.Subs[0].Should().BeOfType<Article>();
      doc.Subs[1].Should().BeOfType<Article>();
      ((Article) doc.Subs[0]).Title.Should().Be("Heading");
      ((Article) doc.Subs[1]).Title.Should().Be("Heading 2");
    }

    [Fact]
    void HeadingBecomesArticle() {
      const String input = @"# Heading
Paragraph";
      var doc = MainBuilder.CreateDefault().Build(input);

      doc.Subs.Count.Should().Be(1);
      var article = (Article) doc.Subs[0];
      article.Title.Should().Be("Heading");
      article.Subs[1].As<Paragraph>().Title.Should().Be("Paragraph");
    }
  }
}
