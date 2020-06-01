'use strict'

class ForkdownMenu {
  init = () => {
    { // Click events 
      $("nav > ul > li").on("click", (e) => {
        this.closeMenus()
        $(e.currentTarget).addClass("fd--active")
        e.stopPropagation()
      })
      $("nav > ul > li > ul a").on("click", this.closeMenus)
      $("body").on("click", this.closeMenus)
    }

    this.positionMenus()
    $(window).on("resize", this.positionMenus)

    return this;
  }

  closeMenus = () => {
    $("nav > ul > li").removeClass("fd--active")
  }

  positionMenus = () => {
    let menus = $("nav > ul > li > ul")
    menus.get().forEach((m) => {
      m = $(m)
      let parent = m.closest("li")
      m.css("left", parent.offset().left + 'px')
      m.css("top", (parent.offset().top + parent.outerHeight()) + 'px')
    })
  }

}