using System;
using Forkdown.Core.Main.Parsing.MarkdigExtensions;
using Markdig;
using Markdig.Syntax;

namespace Forkdown.Core.Parsing {
  public static class Markdown {
    public static MarkdownDocument ToDocument(String markdown) {
      var pipeline = new MarkdownPipelineBuilder();
      pipeline.Extensions.AddIfNotAlready<ShorthandLinkExtension>();
      pipeline.UseGenericAttributes();
      return Markdig.Markdown.Parse(markdown, pipeline.Build());
    }
  }
}