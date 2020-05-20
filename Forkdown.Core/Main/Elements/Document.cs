using System;
using System.Linq;
using Markdig.Syntax;
using Simpler.NetCore.Collections;
using Block = Forkdown.Core.Elements.Types.Block;

namespace Forkdown.Core.Elements {
  public class Document : Element, Block {
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