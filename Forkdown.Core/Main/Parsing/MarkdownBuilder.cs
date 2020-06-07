using System;
using Forkdown.Core.Parsing.MarkdigExtensions;
using Markdig;
using Markdig.Extensions.CustomContainers;
using Markdig.Extensions.Tables;
using Markdig.Syntax;

namespace Forkdown.Core.Parsing {
  public static class MarkdownBuilder {
    public static MarkdownDocument DefaultBuild(String markdown) {
      var pipeline = new MarkdownPipelineBuilder();
      pipeline.Extensions.AddIfNotAlready<ShorthandLinkExtension>();
      pipeline.Extensions.AddIfNotAlready<CheckboxExtension>();
      pipeline.Extensions.AddIfNotAlready<CustomContainerExtension>();
      pipeline.Extensions.AddIfNotAlready(new PipeTableExtension());
      pipeline.UseGenericAttributes();
      return Markdown.Parse(markdown, pipeline.Build());
    }
  }
}