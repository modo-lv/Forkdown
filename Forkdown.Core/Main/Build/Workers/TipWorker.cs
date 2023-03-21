using System;
using System.Linq;
using Forkdown.Core.Elements;
using Simpler.NetCore.Collections;

namespace Forkdown.Core.Build.Workers {
  public class TipWorker : Worker {
    public TipWorker() {
      this.RunsAfter<LinesToParagraphsWorker>();
      this.RunsAfter<ItemWorker>();
      this.RunsAfter<HeadingItemWorker>();
    }

    public override Element BuildElement(Element element) {
      switch (element) {
        // Regular
        case ExplicitInlineContainer c when c.Subs.FirstOrDefault() is Text t && !t.Content.Contains(" "): {
          var result = new Tip(t.Content);
          if (this.Context!.Config?.Tips.List.GetOr(result.ExplicitId, null!) is { } tc) {
            result.Icon = new Text(tc.Icon);
            result.Title = tc.Title;
          }
          return result;
        }
        // Item labels
        case Item item when item.Title.Subs.FirstOrDefault() is Code labelCode: {
          item.Title.Subs.RemoveAt(0);

          var labelStrings = labelCode
            .Content
            .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(_ => _.Trim())
            .ToHashSet();
          
          item.Labels = labelStrings
            .Where(l => !l.StartsWith(":"))
            .Select(key => {
              var result = new Tip(key);
              if (this.Context!.Config?.Tips.List.GetOr(key, null!) is { } tc) {
                result.Icon = new Text(tc.Icon);
                result.Title = tc.Title;
              }
              return result;
            })
            .ToList();
          if (item.Title.Subs.FirstOrDefault() is Text tt)
            tt.Content = tt.Content.TrimStart();
          
          return item;
        }
      }

      return element;
    }
  }
}
