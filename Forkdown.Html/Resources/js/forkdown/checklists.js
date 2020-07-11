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
    $(".fd--checkitem > .fd--bullet input[type='checkbox']").each((i, c) => {
      let checkbox = $(c)
      let main = this.main;
      let id = checkbox.prop("id")
      if (!id) {
        console.log(c)
        throw new Error("Checkbox without an ID")
      }

      checkbox.prop("checked", main.profile.isChecked(id))
      checkbox.on("change", async (e) => {
        let c = $(e.target)
        let id = c.prop("id")
        main.profile.toggleCheck(id, c.prop("checked"))
        await this.main.saveProfile()
      })
    })
    
    // Singles
    $("li > .fd--main > article.fd--single").closest("li").addClass("fd--single");

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