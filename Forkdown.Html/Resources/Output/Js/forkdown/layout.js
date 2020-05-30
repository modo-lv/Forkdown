'use strict'

/**
 * Layout asstant, responsible for masonry layout for columns.
 */
class ForkdownLayout {
  constructor() {
    this.defaultColumnWidth = null;
  }

  
  init = () => {
    { // Temporarily create an invisible layout column to get the minimum size in pixels
      let column = $("<div class='fd--column' style='display: none;'></div>")
      $("body").append(column)
      this.defaultColumnWidth = $(column).css("min-width").replace('px', '') * 1
      column.remove()
      //console.debug("Defult article width:", this.defaultArticleWidth, "pixels.")
    }

    let items = $("article.fd--columns > [role='main'] > *, section.fd--columns > *, main.fd--columns > *")
    items.wrap("<div class='fd--column'></div>")

    this.processMasonry()
    $(window).on("resize", this.processMasonry)
  }


  masonryIsOn = () => 
    $(window).width() > this.defaultColumnWidth * 2


  processMasonry = () => {
    $($(".fd--columns")
      .css("grid-auto-rows", this.masonryIsOn() ? "0.5rem" : "")
      .get()
      .reverse() // Resize inner elements first
    ) 
    .children(".fd--column")
    .get()      
    .forEach(this.processMasonryColumn);
  }


  processMasonryColumn = (column) => {
    if (!this.masonryIsOn()) {
      column.style.gridRowEnd = "";
      return;
    }

    //console.groupCollapsed("Resizing", column);
    let grid = $(column).closest(".fd--columns");
    let rowHeight = $(grid).css('grid-auto-rows').replace('px', '') * 1;
    //console.debug("Grid row height:", rowHeight)
    let rowGap = $(grid).css('grid-row-gap').replace('px', '') * 1;
    if (Number.isNaN(rowGap))
      rowGap = 0
    //console.debug("Grid row gap:", rowGap)
    let height = $(column.children[0]).outerHeight()
    //console.debug("Column real height:", height)
    let rowSpan = Math.ceil((height + rowGap) / (rowHeight + rowGap));
    //console.debug("Column row span:", rowSpan)
    column.style.gridRowEnd = "span " + rowSpan;
    //console.groupEnd()
  }
}