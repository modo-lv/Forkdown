'use strict'

/**
 * Layout asstant, responsible for masonry layout for columns.
 */
class ForkdownLayout {
  constructor() {
    this.defaultColumnWidth = null;
  }

  static MainHider = document.createElement("style");

  init = async () => {
    { // Temporarily create an invisible layout column to get the default column width
      let column = $("<div class='fd--column' style='display: none;'></div>")
      $("body").append(column)
      this.defaultColumnWidth = $(column).css("min-width").replace('px', '') * 1
      column.remove()
    }
    { // Wrap columns in divs
      let items = $("article.fd--columns > .fd--main > *, section.fd--columns > *, main.fd--columns > *")
      items.wrap("<div class='fd--column'></div>")
    }

    this.processMasonry()
      .then(ForkdownLayout.showMain)
      .then(this.focusAnchor)
    $(window).on("resize", this.processMasonry)
    $(window).on("hashchange", window.tippy.hideAll)
    $(window).on("hashchange", this.focusAnchor)

    $("main").on("scroll", () => {
      console.log($("main").scrollTop())
    })
  }

  /**
   * Scroll to and focus on the requested element, if any.
   */
  focusAnchor = () => {
    let id = window.location.hash.substr(1)
    if (!id)
      return
    let anchor = "[id='" + id + "']"
    let main = $("main")
    if (anchor !== "") {
      let mainTop = main.offset().top
      let el = $(anchor).closest(":visible")
      let elTop = el.offset().top
      main[0].scrollTop = elTop - mainTop
      el.attr("tabindex", -1).focus()
    }
    else {
      main.attr("tabindex", -1).focus()
    }
  }

  /**
   * @returns {Promise}
   */
  processMasonry = () =>
    this.processMasonryColumns($(".fd--columns, article.fd--columns > .fd--main").children(".fd--column").get())

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
      let grid = column.closest(".fd--columns > .fd--main, .fd--columns");
      if (this.masonryIsOn()) {
        grid.css("grid-auto-rows", "10px")
        let rowHeight = grid.css('grid-auto-rows').replace('px', '') * 1;
        let rowGap = grid.css('grid-row-gap').replace('px', '') * 1;
        if (Number.isNaN(rowGap))
          rowGap = 0
        let height = column.children().first().outerHeight();
        let rowSpan = Math.round((height + rowGap) / (rowHeight + rowGap));
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
  masonryIsOn = () => true
    //$(window).width() > this.defaultColumnWidth * 2


   /**
   * Hide the main content.
   * Use during page load to hide the layout shuffling as things are moved into masonry positions
   */
  static hideMain() {
    ForkdownLayout.MainHider.innerText = ForkdownLayout.MainHider.innerText || "main > * { visibility: hidden; } main { overflow: hidden }"
    document.head.appendChild(ForkdownLayout.MainHider)
  }

  /**
   * Show the main content.
   * Use when the initial masonry layout is finished, to display the content.
   */
  static showMain() {
    document.head.removeChild(ForkdownLayout.MainHider)
  }
}
