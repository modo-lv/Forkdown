'use strict'

class ForkdownMain {
  constructor({projectConfig}) {
    this.profileIds = []
    this.profileId = 0

    this.projectConfig = projectConfig
    this.dbName = "forkdown:" + this.projectConfig.name
    this.profiles = []
    this.profile = {}

    this.storage = localforage.createInstance({
      name: this.dbName,
      storeName: "main"
    })
  }

  async init() {
    await this.load()
    if (this.profiles.length < 1) {
      console.info("No profiles found, creating default...")
      this.createDefaultProfile()
      await this.save()
    }
    return this
  }

  createDefaultProfile() {
    this.profileId = this.addProfile({
      id: 1,
      name: "Default"
    }).id
    return this.profilesUpdated()
  }

  async load() {
    let profileIds = await this.storage.getItem("profileIds") || []
    this.profiles = []
    profileIds.forEach(async pId => {
      let dto = await this.storage.getItem("profiles[" + pId + "]")
      if (dto != null)
        this.addProfile(dto)
    })

    this.profileId = await this.storage.getItem("profileId")
    this.profilesUpdated()

    return this
  }

  async save() {
    await this.storage.setItem("profileIds", this.profileIds)
    await this.storage.setItem("profileId", this.profileId)
    this.profiles.forEach(async prof => await this.saveProfile(prof))

    return this
  }

  async saveProfile(profile = null) {
    if (profile == null)
      profile = this.profile
    if (profile != null)
      await this.storage.setItem("profiles[" + profile.id + "]", profile.toDto())
    return this
  }

  /**
   * @param {ForkdownProfile} dto 
   */
  addProfile(dto) {
    // TODO: Check for existing
    let profile = ForkdownProfile.fromDto(dto)
    this.profiles.push(profile)
    return profile
  }

  profilesUpdated() {
    this.profileIds = this.profiles.map(p => p.id)
    this.profile = this.profiles.find(p => p.id == this.profileId)
    if (!this.profile && this.profiles.length > 0) {
      this.profile = this.profiles[0]
      this.profileId = this.profile.id
    }
    return this
  }
}