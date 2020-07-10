﻿using Forkdown.Core.Elements;

namespace Forkdown.Core.Build.Workers {
  public class DocumentWorker : Worker, IDocumentWorker {

    public override Element ProcessElement(Element element, Arguments args) {
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
