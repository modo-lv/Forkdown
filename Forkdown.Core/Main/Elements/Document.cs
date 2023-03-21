using System;
using System.Linq;
using Forkdown.Core.Elements.Types;
using Simpler.NetCore.Text;

namespace Forkdown.Core.Elements; 

public class Document : BlockContainer {
  /// <summary>
  /// Path of the file that this document was created from, relative to project root.
  /// </summary>
  public String ProjectFilePath {
    get => this._projectFilePath;
    set => this._projectFilePath = value.Replace('\\', '/');
  }
  private String _projectFilePath = "";

  /// <summary>
  /// A unique document identifier, based on the filename.
  /// </summary>
  public String ProjectFileId => this.ProjectFilePath.TrimPrefix("pages/").TrimSuffix(".md");

  /// <summary>
  /// How deep in the project file tree this document is.
  /// </summary>
  public Int32 Depth => this.ProjectFilePath.Count(_ => _ == '/');


  public new Document AddSub(Element sub) { return (Document)base.AddSub(sub); }
  public new Document AddSubs(params Element[] subs) { return (Document)base.AddSubs(subs); }
}