using System;
using Block = Forkdown.Core.Elements.Types.Block;
using Tables = Markdig.Extensions.Tables;

namespace Forkdown.Core.Elements; 

public class TableRow : Element, Block {
  public Boolean IsHeader;

  public TableRow(Tables.TableRow mdo) : base(mdo) {
    this.IsHeader = mdo.IsHeader;
  }
}