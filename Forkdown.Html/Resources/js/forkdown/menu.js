'use strict'

/**
 * Class for manipulating the main menu
 */
class ForkdownMenu {
  static MenuHider = document.createElement("style")

  /**
   * Init
   */
  init = async  () => {
    $("nav > ul > li").get().forEach(li => {
      $(li).addClass("fd--scripted")
      let content = $(li).find("ul").html()
      window.tippy(li, {
        content: content,
        allowHTML: true,
        interactive: true,
        trigger: 'click',
        theme: 'menu',
        arrow: false,
        offset: [0, 0],
        placement: 'bottom-start'
      })
    })

    return this;
  }


  /**
   * Hide the main content.
   * Use during page load to hide the layout shuffling as things are moved into masonry positions
   */
  static hideMain() {
    ForkdownMenu.MenuHider.innerText = ForkdownMenu.MenuHider.innerText || "nav > ul > li > ul { display: none; }"
    document.head.appendChild(ForkdownMenu.MenuHider)
  }
}