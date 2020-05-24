using System;
using Markdig.Syntax;

namespace Forkdown.Core.Elements {
  public class ListItem : Section {

    public Boolean IsCheckbox;

    public ListItem(ListItemBlock mdo) : base(mdo) {
      
    }
  }
}
