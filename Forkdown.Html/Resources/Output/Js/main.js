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
  $(".fd--columns").css("grid-auto-rows", "1.5rem")

  // Find all grid items and add <div>s for sizing
  {
    let items = $("article.fd--columns > [role='main'] > *, section.fd--columns > *, main.fd--columns > *")
    items.wrap("<div class='fd--column'>")
  }

  function resizeGridItem(item){
    let grid = $(item).closest(".fd--columns")[0];
    let rowHeight = parseInt(window.getComputedStyle(grid).getPropertyValue('grid-auto-rows'));
    let rowGap = parseInt(window.getComputedStyle(grid).getPropertyValue('grid-row-gap'));
    let rowSpan = Math.ceil((item.children[0].getBoundingClientRect().height+rowGap)/(rowHeight+rowGap));
    item.style.gridRowEnd = "span "+rowSpan;
  }

  function resizeAllGridItems(grid) {
    let allItems = grid.querySelectorAll(".fd--column");
    for(let x=0;x<allItems.length;x++){
      //console.log("resizing", allItems[x]);
      resizeGridItem(allItems[x]);
    }
  }

  // Reverse so that inner elements get resolved first and properly set the size of their parents
  $($(".fd--columns").get().reverse()).each((i, el) => {
    resizeAllGridItems(el);
    window.addEventListener("resize", () => resizeAllGridItems(el));
  });

})