using Forkdown.Core.Build.Builders;

namespace Forkdown.Core.Build {
  public partial class ForkdownBuild {
    public static ForkdownBuild Default => new ForkdownBuild()
      .AddBuilder<LinesToParagraphsBuilder>()
      .AddBuilder<DocumentAttributesBuilder>()
      .AddBuilder<LinkIndexBuilder>();
  }
}
