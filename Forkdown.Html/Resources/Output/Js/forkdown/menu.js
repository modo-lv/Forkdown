'use strict'

/**
 * Class for manipulating the main menu
 */
class ForkdownMenu {
  init = () => {
    { // Click events 
      $("nav > ul > li").on("click", (e) => {
        let menuIsOpen = $(e.currentTarget).hasClass("fd--active")
        this.closeMenus()
        if (!menuIsOpen)
          $(e.currentTarget).addClass("fd--active")
        e.stopPropagation()
      })
      $("nav > ul > li > ul a").on("click", this.closeMenus)
      $("body").on("click", this.closeMenus)
    }

    { // Position adjustments 
      this.positionMenus()
      $(window).on("resize", this.positionMenus)
    }

    { // With everything set up, remove the CSS hover
      $("nav > ul > li").removeClass("fd--noscript").addClass("fd--script")
    }

    return this;
  }

  /**
   * Close all curently opened sub-menus
   */
  closeMenus = () => {
    $("nav > ul > li").removeClass("fd--active")
    return this;
  }

  /**
   * Adjust sub-menu positions to line up with the main item
   */
  positionMenus = () => {
    let menus = $("nav > ul > li > ul")
    menus.get().forEach((m) => {
      m = $(m)
      let parent = m.closest("li")
      m.css("left", '-1px')
      m.css("top", ((parent.outerHeight() - parent.css("border-width").replace('px', '') * 1)) + 'px')
    })
    return this;
  }

}