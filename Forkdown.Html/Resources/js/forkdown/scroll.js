'use strict'

class ForkdownScroll {
  /**
   * @param {ForkdownProfileSet} profileSet
   */
  constructor({ profileSet }) {
    /** @type {function:ForkdownProfile} **/
    this.profile = () => profileSet.activeProfile
  }

  init = async () => {
    let id = window.location.hash.substr(1)
    if (id.length < 1)
      return

    let main = $("main")

    let pos = this.profile().scrollPosition
    if (pos.id === id) {
      main.scrollTop(pos.position)
    }

    main.on("scroll", (e) =>
      this.profile().saveScrollPosition(id, $(e.target).scrollTop())
    )
    main.trigger("scroll")
  }
}