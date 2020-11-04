class ForkdownProfileSet {
  /** @type {ForkdownStorage} **/
  storage
  /** @type {Array.<ForkdownProfile>} **/
  profiles = []
  /** @type {ForkdownProfile} **/
  activeProfile = null

  /**
   *
   * @param {ForkdownStorage} storage Storage object for saving and loading profile data.
   */
  constructor({storage}) {
    this.storage = storage
  }


  /**
   * @returns {Promise<any>}
   */
  init = async () =>
    this.loadProfiles().then(this.loadActiveProfile).then(() => {
      if (!this.profiles.length) {
        console.info("No profiles found, creating default...")
        this.profiles = [new ForkdownProfile({
          id: 1,
          name: "Default"
        })]
        this.activeProfile = this.profiles[0]
        return this.save()
      }
    })


  /**
   *
   * @return {Promise<any>}
   */
  loadProfiles = async () =>
    this.storage.keys(/^profile\[\d+]$/)
      .then(this.storage.getItems)
      .then(profiles =>
        this.profiles = (profiles || [])
          .sort((a, b) => a.id - b.id)
          .map(p => new ForkdownProfile(p))
      )

  loadActiveProfile = async () =>
    this.storage.getItem("activeProfileId").then(activeId =>
      this.activeProfile = this.profiles.find(_ => _.id === activeId) || this.activeProfile
    )

  // noinspection JSCheckFunctionSignatures
  save = async () =>
    Promise.allSettled([
      this.storage.setItems(Object.fromEntries(this.profiles.map(p => [`profile[${p.id}]`, p]))),
      this.storage.setItem("activeProfileId", this.activeProfile.id)
    ])
}