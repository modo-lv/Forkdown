class ForkdownScroll {
  /**
   *
   * @param {ForkdownMain} main
   */
  constructor({main = null}) {
    if (!(main instanceof ForkdownMain))
      throw new Error("Cannot initialize without ForkdownMain.")
    this.main = main
  }

  init = async () => {
    let id = window.location.hash.substr(1)
    if (id.length < 1)
      return

    let anchor = document.getElementById(id)

    $("main").on("scroll", (e) => {
      $(e.target).scrollTop
    })
  }
}