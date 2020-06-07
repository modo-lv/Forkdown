using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Forkdown.Core.Elements;
using Forkdown.Core.Parsing.Forkdown.Processors;
using Simpler.NetCore.Collections;
using Path = Fluent.IO.Path;

namespace Forkdown.Core.Parsing.Forkdown {
  public partial class ForkdownBuilder {
    public readonly IList<IElementProcessor> Chain = Nil.L<IElementProcessor>();

    public Document Build(String markdown, ProjectPath? file = null) {
      var mDoc = MarkdownBuilder.DefaultBuild(markdown);
      var fDoc = (Document) FromMarkdown.ToForkdown(mDoc);
      fDoc.ProjectFilePath = file?.RelPathString() ?? "";
      return this.Build(fDoc);
    }

    public Document Build(Document document) {
      // Document processors
      document = this.Chain.Where(_ => _ is IDocumentProcessor)
        .Aggregate(document, (doc, p) => ((IDocumentProcessor) p).Process(doc));

      return process(document, Nil.DStr<Object>());

      T process<T>(T element, IDictionary<String, Object> context) where T : Element {
        element = this.Chain
          .Where(_ => !(_ is IDocumentProcessor))
          .Aggregate(element, (e, p) => p.Process(e, context));
        element.Subs = element.Subs.Select(e => process(e, new Dictionary<String, Object>(context))).ToList();
        return element;
      }
    }


    public ForkdownBuilder AddProcessor<T>() where T : IElementProcessor, new() {
      this.Chain.Add(new T());
      return this;
    }
  }
}
