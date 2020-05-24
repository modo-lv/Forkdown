﻿using System;
using System.Linq;
using Simpler.NetCore.Text;
using Block = Forkdown.Core.Elements.Types.Block;

namespace Forkdown.Core.Elements {
  public partial class Document : Element, Block {
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
  }
}