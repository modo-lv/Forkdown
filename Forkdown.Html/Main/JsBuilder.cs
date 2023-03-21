using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Forkdown.Core.Wiring;
using Microsoft.Extensions.Logging;
using Simpler.NetCore.Collections;
using Path = Fluent.IO.Path;

namespace Forkdown.Html.Main; 

/// <summary>
/// Build JavaScript files for a Forkdown-HTML project.
/// </summary>
public class JsBuilder {
  private readonly BuildArguments _args;
  private readonly Path _inPath;
  private readonly Path _outPath;
  private readonly ILogger<JsBuilder> _logger;
  /// <inheritdoc cref="JsBuilder"/>
  public JsBuilder(BuildArguments args, ILogger<JsBuilder> logger) {
    _args = args;
    _logger = logger;
    _inPath = Program.InPath.Combine("js");
    _outPath = _args.ProjectRoot.Combine(Program.OutFolder);
  }

    
    
  /// <summary>
  /// List of all script files for the project (relative paths).
  /// </summary>
  public IList<String> ScriptPaths {
    get {
      var list = _inPath.Files("*.js", true)
        .Select(_ => _.MakeRelativeTo(this._inPath).ToString().Replace("\\", "/"))
        .ToList();
      { // main.js must be the last file in the list
        list.Remove("main.js");
        list.Add("main.js");
      }
      return list;
    }
  }



  /// <summary>
  /// Process input files and create outputs. 
  /// </summary>
  public JsBuilder Build() {
    _logger.LogInformation("Building {js}...", "JavaScript");
    this.ScriptPaths.ForEach(path => {
      var inFile = _inPath.Combine(path);
      var outFile = _outPath.Combine(path);
      _logger.LogDebug("Copying {file}...", path);
        
      outFile.Parent().CreateDirectories();
      File.Copy(inFile.ToString(), outFile.ToString(), true);
    });
      
    return this;
  }
}