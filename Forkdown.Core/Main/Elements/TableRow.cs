using System;
using Block = Forkdown.Core.Elements.Types.Block;
using Markdown = Markdig.Extensions.Tables;

namespace Forkdown.Core.Elements {
  public class TableRow : Element, Block {
    public Boolean IsHeader;

    public TableRow(Markdown.TableRow mdo) : base(mdo) {
      this.IsHeader = mdo.IsHeader;
    }
  }
}
