'use strict'

/**
 * Class for manipulating the main menu
 */
class ForkdownMenu {
  static MenuHider = document.createElement("style")

  /**
   * Init
   */
  init = async () => {
    $("nav > ul > li").get().forEach(li => {
      $(li).addClass("fd--scripted")
      let content = $(li).find("ul").html()
      window.tippy(li, {
        content: content,
        allowHTML: true,
        interactive: true,
        trigger: 'click mouseenter focus',
        theme: 'light-border',
        arrow: false,
        offset: [0, 0],
        placement: 'bottom-start',
        maxWidth: 'none',
      })
    })

    return this
  }


  /**
   * Hide the main menu (sub) items.
   * Use during page load to collapse the menu entries to their headings.
   */
  static hideMain() {
    ForkdownMenu.MenuHider.innerText = ForkdownMenu.MenuHider.innerText || "nav > ul > li > ul { display: none; }"
    document.head.appendChild(ForkdownMenu.MenuHider)
  }
}