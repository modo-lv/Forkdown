using System;
using Markdig;
using Markdig.Syntax;

namespace Forkdown.Core.Main.Parsing {
  public class MarkdownParse {
    public static MarkdownDocument ToDocument(String markdown) {
      var pipeline = new MarkdownPipelineBuilder();
      pipeline.Extensions.AddIfNotAlready<MarkdownShorthandLinkExtension>();
      return Markdown.Parse(markdown, pipeline.Build());
    }
  }
}