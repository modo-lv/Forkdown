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

  // Set  row height on every grid
  $("article.fd--grid > [role='main'], section.fd--grid, main.fd--grid").css("grid-auto-rows", "1.5rem")

  // Find all grid items and add <div>s for sizing
  {
    let items = $("article.fd--grid > [role='main'] > *, section.fd--grid > *, main.fd--grid > *")
    items.wrap("<div class='fd--mortar'>")
  }

  function resizeGridItem(item){
    let grid = document.getElementsByClassName("fd--grid")[0];
    let rowHeight = parseInt(window.getComputedStyle(grid).getPropertyValue('grid-auto-rows'));
    let rowGap = parseInt(window.getComputedStyle(grid).getPropertyValue('grid-row-gap'));
    let rowSpan = Math.ceil((item.children[0].getBoundingClientRect().height+rowGap)/(rowHeight+rowGap));
    item.style.gridRowEnd = "span "+rowSpan;
  }

  function resizeAllGridItems(grid) {
    let allItems = grid.querySelectorAll(".fd--mortar");
    for(let x=0;x<allItems.length;x++){
      console.log("resizing", allItems[x]);
      resizeGridItem(allItems[x]);
    }
  }

  $(".fd--grid").each((i, el) => {
    resizeAllGridItems(el);
    window.addEventListener("resize", () => resizeAllGridItems(el));
  });

})