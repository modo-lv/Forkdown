'use strict'

/**
 * Layout asstant, responsible for masonry layout for columns.
 */
class ForkdownLayout {
  constructor() {
    this.defaultColumnWidth = null;
  }

  static MainHider = document.createElement("style");

  /**
   * Hide the main content.
   * Use during page load to hide the layout shuffling as things are moved into masonry positions
   */
  static hideMain() {
    ForkdownLayout.MainHider.innerText = "main > * { visibility: hidden; } main { overflow: hidden }"
    document.head.appendChild(ForkdownLayout.MainHider)
  }

  /**
   * Show the main content.
   * Use when the initial masonry layout is finished, to display the content.
   */
  static showMain() {
    document.head.removeChild(ForkdownLayout.MainHider)
  }

  init = async () => {
    { // Temporarily create an invisible layout column to get the default column width
      let column = $("<div class='fd--column' style='display: none;'></div>")
      $("body").append(column)
      this.defaultColumnWidth = $(column).css("min-width").replace('px', '') * 1
      column.remove()
    }
    { // Wrap columns in divs
      let items = $("article.fd--columns > [role='main'] > *, section.fd--columns > *, main.fd--columns > *")
      items.wrap("<div class='fd--column'></div>")
    }

    this.processMasonry().then(ForkdownLayout.showMain)
    $(window).on("resize", this.processMasonry)
  }

  /**
   * @returns {Promise}
   */
  processMasonry = () =>
    this.processMasonryColumns($(".fd--columns").children(".fd--column").get())

  /**
   * @param {Array<HTMLElement>} columns
   * @returns {Promise}
   */
  processMasonryColumns = (columns) => new Promise((resolve) => {
    if (columns.size < 1) {
      resolve();
      return
    }

    for (let a = 0; a < columns.length; a++) {
      let column = $(columns[a])
      let grid = column.closest(".fd--columns");
      if (this.masonryIsOn()) {
        grid.css("grid-auto-rows", "0.5rem")
        let rowHeight = grid.css('grid-auto-rows').replace('px', '') * 1;
        let rowGap = grid.css('grid-row-gap').replace('px', '') * 1;
        if (Number.isNaN(rowGap))
          rowGap = 0
        let height = column.children().first().outerHeight();
        let rowSpan = Math.floor((height + rowGap) / (rowHeight + rowGap));
        column.css("grid-row-end", "span " + rowSpan)
      }
      else {
        grid.css("grid-auto-rows", "")
        column.css("grid-row-end", "")
      }
    }

    resolve()
  })

  /**
   * @returns {Boolean}
   */
  masonryIsOn = () => 
    $(window).width() > this.defaultColumnWidth * 2
}
