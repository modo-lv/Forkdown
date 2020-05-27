'use strict'

let projectName = $("body").data("project-name")

if (!projectName)
  throw new Error("Forkdown script can't function without a valid project name.");

new ForkdownMain({projectConfig: {name: projectName}}).init().then(main => {
  // CHECKBOXES
  $("li.fd--checkbox input[type=checkbox]").each(async (i, c) => {
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

$(() => {
  // Find all masonry items and add <div>s for sizing
  {
    let items = $(".fd--masonry section, .fd--masonry ")
  }

  /*
  function resizeGridItem(item){
    let grid = document.getElementsByTagName("main")[0];
    let rowHeight = parseInt(window.getComputedStyle(grid).getPropertyValue('grid-auto-rows'));
    let rowGap = parseInt(window.getComputedStyle(grid).getPropertyValue('grid-row-gap'));
    let rowSpan = Math.ceil((item.querySelector('div').getBoundingClientRect().height+rowGap)/(rowHeight+rowGap));
    item.style.gridRowEnd = "span "+rowSpan;
  }

  function resizeAllGridItems(){
    let allItems = document.querySelectorAll("main > section");
    for(let x=0;x<allItems.length;x++){
      console.log("resizing", allItems[x]);
      resizeGridItem(allItems[x]);
    }
  }

  resizeAllGridItems();
  */
})