using System;
using System.Linq;
using Forkdown.Core.Main.Elements;
using Markdig.Syntax;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Elements {
  public class Document : Container {
    private String _fileName = "";
    public String FileName
    {
      get => this._fileName;
      set => this._fileName = value.Replace('\\', '/');
    }

    /// <summary>
    /// How deep in the project file tree this document is.
    /// </summary>
    public Int32 Depth => this.FileName.Count(_ => _ == '/');

    public Document(MarkdownDocument document) : base(document) { }
  }
}