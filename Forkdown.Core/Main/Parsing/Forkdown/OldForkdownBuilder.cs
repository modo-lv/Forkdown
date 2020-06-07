/*
using System;
using System.Collections.Generic;
using System.IO;
using Forkdown.Core.Elements;
using Forkdown.Core.Parsing.Forkdown.OldProcessors;
using Simpler.NetCore.Collections;
using Path = Fluent.IO.Path;

namespace Forkdown.Core.Parsing.Forkdown {
  public partial class OldForkdownBuilder {
    public readonly IList<IForkdownProcessor> Chain = Nil.L<IForkdownProcessor>();

    public OldForkdownBuilder AddProcessor<T>() where T : IForkdownProcessor, new() {
      this.Chain.Add(new T());
      return this;
    }

    public Document Build(String markdown, String fileName = "") {
      var doc = Document.From(markdown);
      doc.ProjectFilePath = fileName;
      this.Chain.ForEach(_ => _.Process(doc));
      return doc;
    }

    public Document Build(Path file, String fileName) {
      using var reader = new FileInfo(file.FullPath).OpenText();
      return this.Build(reader.ReadToEnd(), fileName ?? file.ToString());
    }
  }
}
*/
