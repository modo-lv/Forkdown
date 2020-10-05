'use strict'

let projectName = $("body").data("project-name")

if (!projectName)
  throw new Error("Forkdown script can't function without a valid project name.");

let main = new ForkdownMain({ projectConfig: { name: projectName } })
let checklists = new ForkdownChecklists(main)
let layout = new ForkdownLayout();
let menu = new ForkdownMenu();
let meta = new ForkdownMetaText();

main.init()
  .then(meta.init)
  .then(checklists.init)
  .then(menu.init)
  .then(layout.init)