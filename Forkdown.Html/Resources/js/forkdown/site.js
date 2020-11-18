class ForkdownSite {
  /**
   *
   * @param {ForkdownConfig} config
   * @param {ForkdownProfileSet} profiles
   */
  constructor({ config, profiles }) {
    this.config = config
    this.profiles = profiles
  }

  init = () => {
    let fontApi = document["fonts"]
    if (fontApi && fontApi.ready) {
      fontApi.ready.then(() =>
        document.head.removeChild(ForkdownSite.mainHider)
      )
    }
    else {
      document.head.removeChild(ForkdownSite.mainHider)
    }
  }

  static mainHider = null

  static loading = () => {
    let style = document.createElement("style")
    style.textContent = "#fd--body { visibility: hidden; } #fd--loading { display: block; }"
    document.head.append(style)
    ForkdownSite.mainHider = style
  }
}

ForkdownSite.loading()