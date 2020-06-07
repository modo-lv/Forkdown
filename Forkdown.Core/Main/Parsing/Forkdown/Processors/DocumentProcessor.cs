using System;
using System.Collections.Generic;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Parsing.Forkdown.Processors {
  public class DocumentProcessor : IDocumentProcessor {

    public Document Process(Document doc) {
      return this.Process(doc, Nil.DStr<Object>());
    }

    public T Process<T>(T element, IDictionary<String, Object> args) where T : Element {
      if (element is Document doc &&
          doc.Subs.Count > 0 &&
          doc.Subs[0] is Paragraph par &&
          par.Subs.Count == 1 &&
          par.Subs[0] is Text text &&
          text.Content == ":") {
        par.MoveAttributesTo(doc);
        doc.Subs.Remove(par);
      }
      return element;
    }
  }

}
