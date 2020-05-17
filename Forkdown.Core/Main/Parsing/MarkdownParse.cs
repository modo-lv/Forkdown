using System;
using Forkdown.Core.Main.Parsing.MarkdigExtensions;
using Markdig;
using Markdig.Syntax;

namespace Forkdown.Core.Main.Parsing {
  public static class MarkdownParse {
    public static MarkdownDocument ToDocument(String markdown) {
      var pipeline = new MarkdownPipelineBuilder();
      pipeline.Extensions.AddIfNotAlready<ShorthandLinkExtension>();
      pipeline.UseGenericAttributes();
      return Markdown.Parse(markdown, pipeline.Build());
    }
  }
}