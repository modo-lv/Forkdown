'use strict'

/**
 * Main class for managing profiles.
 */
class ForkdownProfileSet {
  /**
   *
   * @param {ForkdownStorage} storage Storage object for saving and loading profile data.
   */
  constructor({ storage }) {
    this.storage = storage
    this.profiles = []
    this.activeProfile = null
  }


  /**
   * @returns {Promise<any>}
   */
  init = async () =>
    this.loadAllProfiles().then(this.loadActiveProfile).then(() => {
      if (this.profiles.length < 1) {
        console.info("No profiles found, creating default...")
        this.profiles = [ new ForkdownProfile({
          id: 1,
          name: "Default",
          profileSet: this,
        }) ]
        this.activeProfile = this.profiles[0]
        return this.saveAllProfiles()
      }
    })


  /**
   * @param {Number} id
   * @return {ForkdownProfile}
   */
  profile = ({ id }) => {
    let profile = this.profiles.find(p => p.id === id)
    if (!profile)
      throw new Error(`There is no profile with ID ${id}.`)
    return profile
  }


  /**
   *
   * @param {ForkdownProfile} profile ID will be ignored and auto-assigned
   * @param {Boolean} save Automatically save the new profile in storage?
   * @return {Promise<any>}
   */
  newProfile = async ({ profile, save = true }) => {
    profile.id = Math.max.apply(this, this.profiles.map(_ => _.id)) + 1
    this.profiles.push(profile)
    console.info("Added a new profile: [%d] [%s]", profile.id, profile.name)
    if (save)
      return this.saveProfile(profile)
    return profile
  }


  deleteActiveProfile = async () => {
    if (this.profiles.length <= 1) {
      console.info("Cannot remove the last profile.")
      return
    }

    let pi = this.profiles.findIndex((p) =>
      p.id === this.activeProfile.id
    )
    console.log("Removing profile: [%d] %s", this.profiles[pi].id, this.profiles[pi].name)
    this.profiles.splice(pi, 1)
    return this.saveAllProfiles()
  }


  /**
   *
   * @return {Promise<any>}
   */
  loadAllProfiles = async () =>
    this.storage.keys(/^profile\[\d+]$/)
      .then(this.storage.getItems)
      .then(profiles =>
        this.profiles = (profiles || [])
          .sort((a, b) => a.id - b.id)
          .map(p => new ForkdownProfile(p, this))
      )

  loadActiveProfile = async () => {
    return this.storage.getItem("activeProfileId").then(activeId =>
      this.activeProfile = this.profiles.find(_ => _.id === activeId) || this.activeProfile || this.profiles[0]
    )
  }

  /**
   * @param {ForkdownProfile} profile
   * @return {Promise<ForkdownProfile>}
   */
  saveProfile = async (profile = this.activeProfile) => {
    await this.storage.setItem(profile.storageKey(), profile).then(() =>
      this.storage.setItem("activeProfileId", this.activeProfile.id)
    )
    return profile
  }

  saveAllProfiles = async () => {
    // Remove any deleted items
    let cleanup = this.storage.keys(/^profile\[\d+]$/)
      .then(keys => keys.forEach(key => {
        if (!this.profiles.find(p => p.storageKey() === key)) {
          console.debug("Deleting record " + key + " since the profile has been deleted.")
          this.storage.removeItem(key)
        }
      }))

    // noinspection JSCheckFunctionSignatures
    return Promise.allSettled([
      cleanup,
      this.storage.setItems(Object.fromEntries(this.profiles.map(p => [ p.storageKey(), p ]))),
      this.storage.setItem("activeProfileId", this.activeProfile.id)
    ])
  }
}