'use strict'

class ForkdownChecklists {
  /**
   * 
   * @param {ForkdownMain} main 
   */
  constructor(main) {
    this.main = main
  }

  
  /**
   * 
   */
  init = () => new Promise(resolve => {
    // CHECKBOXES
    $(".fd--checkitem > .fd--bullet > input[type='checkbox']").each((i, c) => {
      let checkbox = $(c)
      let main = this.main;
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

    // Meta - X
    let xs = $(".fd--x.fd--title")
    let icon = xs.first().children(".fd--meta-label").text()
    xs.each((i, x) => {
      let button = $("<span>").addClass("fd--x").html(icon)
      let content = $(x).find("p").html()
      $(x).closest(".fd--content").find(".fd--meta").first().append(button)
      $(x).hide();

      window.tippy(button.get(), {
        content: content,
        allowHTML: true,
        interactive: true,
        placement: 'bottom-start',
        trigger: 'click',
      })
    })

    resolve();
  })

  /**
   * 
   * @param {HTMLElement} container 
   */
  static totals(container) {
    let total = $(".fd--checkitem > input[type='checkbox']")
    let checked = ("*[checked]", total);
    console.log(checked, "/", total);
  }
}