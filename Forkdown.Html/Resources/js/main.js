'use strict'

let config = new ForkdownConfig({
  projectName: $("body").data("project-name").toString()
})
// noinspection JSUnresolvedVariable
let storage = new ForkdownStorage({
  localForage: localforage,
  dbName: config.projectName,
  version: "0.1"
})
let profiles = new ForkdownProfileSet({
  storage: storage
})
let site = new ForkdownSite({
  config: config,
  profiles: profiles
})
let items = new ForkdownItems({
  profile: () => profiles.activeProfile,
  storage
})
let menu = new ForkdownMenu()

storage.init()
  .then(profiles.init)
  .then(site.init)
  .then(items.init)
  .then(menu.init)


/*

let main = new ForkdownMain({projectConfig: {name: projectName}})
let checklists = new ForkdownItems(main)
let layout = new ForkdownLayout()
let menu = new ForkdownMenu()
let meta = new ForkdownMetaText()
let settings = new ForkdownSettings(main)
let scroll = new ForkdownScroll({main: main})

main.init()
  .then(meta.init)
  .then(checklists.init)
  .then(menu.init)
  .then(layout.init)
  .then(settings.init)
  .then(scroll.init)
 */