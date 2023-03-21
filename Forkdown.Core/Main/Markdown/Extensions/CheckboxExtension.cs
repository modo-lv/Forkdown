using Markdig;
using Markdig.Parsers;
using Markdig.Renderers;

namespace Forkdown.Core.Markdown.Extensions; 

public class CheckboxExtension : IMarkdownExtension {

  public void Setup(MarkdownPipelineBuilder pipeline) {
    pipeline.BlockParsers.Find<ListBlockParser>()?.ItemParsers.AddIfNotAlready<CheckboxParser>();

  }
  public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer) { }

}