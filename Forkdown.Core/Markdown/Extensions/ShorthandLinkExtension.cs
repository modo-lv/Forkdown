using Markdig;
using Markdig.Parsers.Inlines;
using Markdig.Renderers;

namespace Forkdown.Core.Markdown.Extensions;

/// <summary>
/// Markdown pipeline extension that adds <see cref="ShorthandLinkParser"/>.  
/// </summary>
public class ShorthandLinkExtension : IMarkdownExtension {
  public void Setup(MarkdownPipelineBuilder pipeline) {
    if (!pipeline.InlineParsers.Contains<ShorthandLinkParser>()) {
      pipeline.InlineParsers.InsertBefore<LinkInlineParser>(new ShorthandLinkParser());
    }
  }

  public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer) {
    throw new NotImplementedException();
  }
}