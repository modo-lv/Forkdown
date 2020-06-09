using Markdig;
using Markdig.Parsers.Inlines;
using Markdig.Renderers;

namespace Forkdown.Core.Markdown.Extensions {
  public class ShorthandLinkExtension : IMarkdownExtension {
    public void Setup(MarkdownPipelineBuilder pipeline) {
      if (!pipeline.InlineParsers.Contains<ShorthandLinkParser>())
      {
        pipeline.InlineParsers.InsertBefore<LinkInlineParser>(new ShorthandLinkParser());
      }
    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer) { }
  }
}