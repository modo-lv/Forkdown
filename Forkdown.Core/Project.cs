using System.IO.Abstractions;
using Forkdown.Core.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Simpler.Net.IO;
using Simpler.Net.Main;
using YamlDotNet.Serialization;

namespace Forkdown.Core;

/// <summary>
/// Represents a single Forkdown project.
/// </summary>
/// <remarks>
/// A Forkdown project is, at its core, just a set of Markdown pages to be turned into some output (for example, HTML).
/// </remarks>
public class Project {

  /// <summary>
  /// Path to the root folder that the project files are in.
  /// </summary>
  public readonly IDirectoryInfo Root;

  /// <summary>
  /// Default filename of the main build configuration.
  /// </summary>
  public const String DefaultCoreConfigFileName = "forkdown.core.yaml";

  /// <inheritdoc cref="CoreConfig"/>
  public readonly CoreConfig CoreConfig;

  /// <summary>
  /// Project-scoped services.
  /// </summary>
  public readonly IServiceScope ServiceScope;

  private readonly ILogger<Project> Logger;

  public T Service<T>() where T : notnull => this.ServiceScope.ServiceProvider.GetRequiredService<T>();

  /// <inheritdoc cref="Project"/>
  /// <param name="root"><see cref="Root"/></param>
  /// <param name="coreConfigFile">Path to the file containing core configuration.</param>
  public Project(IDirectoryInfo root, IFileInfo coreConfigFile) {
    this.ServiceScope = Global.ServiceProvider.CreateScope();
    this.Logger = this.Service<ILogger<Project>>();
    this.Root = root;
    this.CoreConfig = coreConfigFile.Let(file => {
      if (file.Exists)
        return new DeserializerBuilder().Build()
          .Deserialize<CoreConfig>(coreConfigFile.ReadAllText());

      this.Logger.LogInformation("Core configuration file {File} not found, using defaults.", coreConfigFile.FullName);
      return new CoreConfig { Name = root.Name };
    });
  }

  ~Project() {
    this.Logger.LogDebug("Project finalized, disposing of service scope.");
    this.ServiceScope.Dispose();
  }
}