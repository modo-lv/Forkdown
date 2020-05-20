using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Forkdown.Core.Config;
using Forkdown.Core.Elements;
using Forkdown.Core.Parsing;
using Forkdown.Core.Wiring;
using Microsoft.Extensions.Logging;
using Simpler.NetCore.Collections;
using Simpler.NetCore.Text;
using YamlDotNet.Core.Tokens;
using Path = Fluent.IO.Path;
// ReSharper disable UnusedMember.Global

namespace Forkdown.Core {
  /// <summary>
  /// Main Forkdown project class.
  /// </summary>
  public class Project {
    /// <inheritdoc cref="MainConfig"/>
    public MainConfig Config = new MainConfig();

    /// <inheritdoc cref="MainConfig.Name"/>
    public String Name => this.Config.Name;

    /// <summary>
    /// Project's Forkdown documents, parsed and processed.
    /// </summary>
    public ISet<Document> Pages = Nil.S<Document>();

    /// <summary>
    /// Index mapping anchors to the pages they are in. 
    /// </summary>
    public IDictionary<String, Document> Anchors = Nil.DStr<Document>(); 
    

    /// Constructor
    private readonly ILogger<Project> _logger;
    private readonly BuildArguments _args;
    public Project(ILogger<Project> logger, BuildArguments args) {
      _logger = logger;
      _args = args;
    }


    /// <summary>
    /// Load the project, including processing all the Forkdown pages. 
    /// </summary>
    /// <exception cref="DirectoryNotFoundException">
    /// If the project directory doesn't exist or isn't a directory.
    /// </exception>
    public Project Load() {
      var root = _args.ProjectRoot;

      if (!root.Exists)
        throw new DirectoryNotFoundException($"Project location not found: {root}");
      if (!root.IsDirectory)
        throw new DirectoryNotFoundException($"Project location is not a directory: {root}");
      _logger.LogInformation("Loading project from {dir}...", root.ToString());

      // Settings
      this.Config = MainConfig.From(_args.MainConfigFile);
      this.Config.Name = this.Config.Name.NonBlank() ?? root.FileName;

      // Pages
      _logger.LogInformation("Finding and loading pages...");
      this.Pages = root.Combine("pages")
        .Files("*.md", true)
        .Select(doc => {
          var relative = doc.MakeRelativeTo(root);
          _logger.LogDebug("Loading {doc}...", relative.ToString());
          return Parsing.BuildForkdown.From(
            doc,
            relative.ToString().TrimSuffix(".md", StringComparison.InvariantCultureIgnoreCase)
          );
        })
        .ToHashSet();
      
      // Achors
      this.Anchors = AnchorIndex.BuildFrom(this.Pages);
      

      _logger.LogInformation("Project \"{name}\" loaded, {pages} page(s).", this.Config.Name, this.Pages.Count);
      return this;
    }
  }
}
