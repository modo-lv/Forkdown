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
let profiles = new ForkdownProfileSet({ storage: storage })
let site = new ForkdownSite({
  config: config,
  profiles: profiles
})
let items = new ForkdownItems({ profileSet: profiles })
let menu = new ForkdownMenu()
let meta = new ForkdownMetaText()
let settings = new ForkdownSettings({
  profileSet: profiles,
  storage: storage
})
let scroll = new ForkdownScroll({ profileSet: profiles })

storage.init()
  .then(profiles.init)
  .then(items.init)
  .then(menu.init)
  .then(meta.init)
  .then(settings.init)
  .then(scroll.init)
  .then(site.init)
