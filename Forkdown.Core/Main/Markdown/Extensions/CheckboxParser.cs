using System;
using Markdig.Parsers;

namespace Forkdown.Core.Markdown.Extensions; 

public class CheckboxParser : OrderedListItemParser {

  public CheckboxParser() {
    this.OpeningCharacters = new[] { 'x' };
  }

  public override Boolean TryParse(BlockProcessor state, Char pendingBulletType, out ListInfo result) {
    result = new ListInfo();

    if (state.PeekChar(1) != ' ')
      return false;

    state.NextChar();

    return true;
  }
}