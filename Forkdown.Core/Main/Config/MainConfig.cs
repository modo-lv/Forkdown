using Forkdown.Core.Config.Input;

namespace Forkdown.Core.Config; 

/// <summary>
/// Main build configuration.
/// </summary>
public partial class MainConfig : MainConfigInput {
  /// <inheritdoc cref="ExternalLinkConfig"/>
  public new ExternalLinkConfig ExternalLinks { get; set; } = new ExternalLinkConfig();

    
  /// <inheritdoc cref="MainConfig"/>
  public MainConfig() { }

  /// <inheritdoc cref="MainConfig"/>
  // ReSharper disable once UnusedMember.Global
  public MainConfig(MainConfigInput input) {
    this.Name = input.Name;
    this.Labels = input.Labels;
    this.Tips = input.Tips;
    this.ExternalLinks = input.ExternalLinks.ToConfig();
  }
}