'use strict'

let projectName = $("body").data("project-name")

if (!projectName)
  throw new Error("Forkdown script can't function without a valid project name.");

new ForkdownMain({projectConfig: {name: projectName}}).init().then(main => {
  // CHECKBOXES
  $("li.checkbox input[type=checkbox]").each(async (i, c) => {
    let checkbox = $(c)
    let id = checkbox.prop("id")
    if (!id) {
      console.log(c)
      throw new Error("Checkbox without an ID")
    }

    checkbox.prop("checked", (main.profile.checked || {})[id] || false)
    checkbox.on("change", async (e) => {
      let c = $(e.target)
      let id = c.prop("id")
      if (c.prop("checked"))
        main.profile.checked[id] = true
      else if (id in main.profile.checked) {
        delete main.profile.checked[id]
      }
      main.saveProfile()
    })
  })
})


// MENU
$("nav a[href]").filter((i, e) => e.href == window.location).parent().parent().addClass("current")

