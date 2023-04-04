using Forkdown.Core.Markdown.Extensions;
using Markdig;
using Markdig.Extensions.CustomContainers;
using Markdig.Extensions.Tables;
using Markdig.Syntax;

namespace Forkdown.Core.Markdown;

/// <summary>
/// Builder(s) for creating <see cref="MarkdownDocument"/> objects from raw text.
/// </summary>
/// <remarks>
/// In order to create a Forkdown document, first we must parse the raw text into a programmatically useful
/// <see cref="MarkdownDocument"/> object.
/// </remarks>
public static class MarkdownBuilder {
  /// <summary>
  /// Markdown parsing (build) pipeline with default configuration.
  /// </summary>
  private static readonly MarkdownPipeline DefaultPipeline =
    new MarkdownPipelineBuilder().Also(p => {
      p.Extensions.AddIfNotAlready<ShorthandLinkExtension>();
      p.Extensions.AddIfNotAlready<CustomContainerExtension>();
      p.Extensions.AddIfNotAlready(new PipeTableExtension());
      p.UseGenericAttributes();
    }).Build();

  /// <summary>
  /// Build a <see cref="MarkdownDocument"/> from a Markdown text, using the default build settings. 
  /// </summary>
  /// <param name="markdown">Markdown text to compile into a Markdown document.</param>
  /// <returns>The created Markdown document</returns>
  public static MarkdownDocument DefaultBuild(String markdown) {
    return Markdig.Markdown.Parse(markdown, DefaultPipeline);
  }
}