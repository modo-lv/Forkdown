using System.Collections.Generic;
using System.Linq;
using Forkdown.Core.Elements;

namespace Forkdown.Core.Build.Workers {
  /// <summary>
  /// Splits forkdown content into articles based on headings.
  /// </summary>
  public class ArticleWorker : Worker {

    public override T ProcessElement<T>(T element, Arguments args) {
      if (element is Header)
        return element;

      Article? article = null;
      Heading? lastHeading = null;
      IEnumerable<Element> subs = element.Subs;

      var newSubs = subs.TakeWhile(_ => _ is Header).ToList();
      foreach (Element el in subs.SkipWhile(_ => _ is Header)) {
        var startNewArticle = el is Heading h && h.Level <= (lastHeading?.Level ?? 7);

        if (article == null) {
          if (startNewArticle) {
            article = new Article((Heading) el);
          }
          else {
            newSubs.Add(el);
          }
        }
        else {
          if (startNewArticle) {
            newSubs.Add(article);
            article = new Article((Heading) el);
          }
          else {
            article.Subs.Add(el);
          }
        }

        if (startNewArticle)
          lastHeading = el as Heading;
      }

      if (article != null) {
        newSubs.Add(article);
      }

      element.Subs = newSubs;
      return element;
    }
    
  }
}
