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

    // Meta
    let meta = cssClass => {
      let metas = $("." + cssClass + ".fd--title")
      let icon = metas.first().children(".fd--meta-label").text()
      metas.each((i, meta) => {
        let button = $("<span>").addClass(cssClass).html(icon)
        let content = $(meta).find("p").html()
        $(meta).closest(".fd--content").find(".fd--meta").first().append(button)
        $(meta).hide();
  
        window.tippy(button.get(), {
          content: content,
          allowHTML: true,
          interactive: true,
          placement: 'bottom-start',
          theme: 'light-border',
          trigger: cssClass == "fd--x" ? 'click' : 'mouseenter focus',
        })
      })
    }
    meta("fd--x")
    meta("fd--info")

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