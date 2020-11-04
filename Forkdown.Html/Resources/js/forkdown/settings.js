'use strict'

class ForkdownSettings {
  /**
   * @param {ForkdownProfileSet} profileSet
   * @param {ForkdownStorage} storage
   */
  constructor({ profileSet: profileSet, storage }) {
    this.profileSet = profileSet
    this.storage = storage
  }

  init = () => {
    this.listProfiles()

    $("#fd--profile-select").on("change", async (e) =>
      this.switchToProfile({ id: Number($(e.target).prop("value")) })
    )

    $("#fd--add-profile").on("click", async () =>
      this.addProfile(window.prompt("Profile name:"))
    )

    $("#fd--edit-profile").on("click", async () =>
      this.renameActiveProfile(window.prompt("Profile name:"))
    )

    $("#fd--clear-profile").on("click", this.resetActiveProfile)

    $("#fd--delete-profile").on("click", this.deleteActiveProfile)

  }


  /**
   * Add a new profile and switch over to it.
   * @param {String} name Name of the profile. Must not be empty.
   * @return {Promise<ForkdownProfile|void>} New profile on success, nothing otherwise.
   */
  addProfile = async (name) => {
    if (!name)
      return

    let newProfile = await this.profileSet.newProfile({
      profile: new ForkdownProfile({ name: name }),
      save: true
    })
    return this.switchToProfile({ id: newProfile.id })
  }


  /**
   * Update (rename) active profile
   * @param {String} name
   * @return {Promise<ForkdownProfile>}
   */
  renameActiveProfile = async (name) => {
    if (!name) {
      return this.profileSet.activeProfile;
    }

    try {
      this.profileSet.activeProfile.name = name
      return this.profileSet.saveProfile()
    } finally {
      this.listProfiles()
    }
  }


  /**
   * Reset current profile (clear all checked items etc.)
   * @return {Promise<ForkdownProfile>}
   */
  resetActiveProfile = async () => {
    console.log("Erasing current profile contents...")
    this.profileSet.activeProfile.items = {}
    return this.profileSet.saveProfile()
  }


  deleteActiveProfile = async () =>
    this.profileSet.deleteActiveProfile().then(this.listProfiles)


  /**
   *
   * @param {Number} id ID of the profile to switch to. 0 means "first in the list", -1 means "last in the list".
   * @return {Promise<void>}
   */
  switchToProfile = async ({ id = 0 }) => {
    try {
      this.profileSet.activeProfile = (id === 0)
        ? this.profileSet.profiles[0]
        : (id === -1)
          ? this.profileSet.profiles[this.profileSet.profiles.length - 1]
          : this.profileSet.profile({ id })
      await this.profileSet.saveProfile()
      console.log("Switched to profile: [%d] %s", this.profileSet.activeProfile.id, this.profileSet.activeProfile.name)
    } finally {
      this.listProfiles()
    }
  }

  listProfiles = () => {
    $("#fd--profile-select option").remove()
    this.profileSet.profiles.forEach(profile => {
      $("#fd--profile-select").append(
        $("<option>")
          .attr("value", profile.id)
          .prop("selected", this.profileSet.activeProfile.id === profile.id)
          .text(profile.name)
      )
    });
  }
}