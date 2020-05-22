using System;
using Forkdown.Core.Parsing.MarkdigExtensions;
using Markdig;
using Markdig.Syntax;

namespace Forkdown.Core.Parsing {
  public static class BuildMarkdown {
    public static MarkdownDocument From(String markdown) {
      var pipeline = new MarkdownPipelineBuilder();
      pipeline.Extensions.AddIfNotAlready<ShorthandLinkExtension>();
      pipeline.Extensions.AddIfNotAlready<CheckboxExtension>();
      pipeline.UseGenericAttributes();
      return Markdown.Parse(markdown, pipeline.Build());
    }
  }
}