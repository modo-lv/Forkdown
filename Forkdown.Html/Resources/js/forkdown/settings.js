'use strict'

class ForkdownSettings {
  constructor(main) {
    this.main = main
  }

  init = () => {
    this.listProfiles()

    $("#fd--delete-profile").on("click", async () => {
      if (this.main.profiles.length > 1) {
        let pi = this.main.profiles.findIndex((prof) => prof.id === this.main.profile.id)
        console.log("Removing profile: " + this.main.profiles[pi].name)
        this.main.profiles.splice(pi, 1)
        this.main.save()
        this.listProfiles()
      }
    })

    $("#fd--edit-profile").on("click", async (e) => {
      let name = window.prompt("Profile name:").toString()
      if (name.length > 0) {
        this.main.profile.name = name
        await this.main.saveProfile()
        this.listProfiles()
      }
    })

    $("#fd--profile-select").on("change", (e) => {
      this.main.profileId = Number($(e.target).prop("value"))
      this.main.profilesUpdated()
      this.main.save()
      this.listProfiles()
      console.log("Switched to profile: %s", this.main.profile.name)
    })

    $("#fd--add-profile").on("click", () => {
      let name = window.prompt("Profile name:").toString()
      if (name.length > 0) {
        let id = Math.max(...this.main.profileIds) + 1
        this.main.addProfile(new ForkdownProfile({
          id: id,
          name: name
        }))
        this.main.profileId = id
        this.main.profilesUpdated()
        this.main.save()
        this.listProfiles()
      }
    })


    $("#fd--clear-profile").on("click", async () => {
      console.log("Erasing current profile contents...")
      this.main.profile.elements = {}
      await this.main.saveProfile()
    })
  }

  listProfiles = () => {
    $("#fd--profile-select option").remove()
    this.main.profiles.forEach(profile => {
      $("#fd--profile-select").append(
        $("<option>")
          .attr("value", profile.id)
          .prop("selected", this.main.profileId === profile.id)
          .text(profile.name)
      )
    });
  }
}