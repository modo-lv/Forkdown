'use strict'

class ForkdownChecklists {
  /**
   *
   * @param {ForkdownMain} main
   */
  constructor(main) {
    this.main = main
    this.index = {}
  }


  /**
   *
   */
  init = () => new Promise(resolve => {
    // Singleton index
    this.index = $("#fd--singles-index").data("index")
    // Singles
    $("li > .fd--main > article.fd--single").each((i, a) => {
        $(a)
          .closest("li")
          .find("> .fd--bullet > input")
          .data("fd--single-id", $(a).data("fd--single-id"))
          .addClass("fd--single")
      }
    )

    // CHECKBOXES
    $(".fd--checkitem > .fd--bullet input[type='checkbox']").each((i, c) => {
      let main = this.main
      let box = $(c)
      let id = box.prop("id")
      if (!id) {
        console.log(c)
        throw new Error("Checkbox without an ID")
      }

      box.on("change", async () => {
        let checked = this.mark(id)
        this.markSingles(id, checked);
        await this.main.saveProfile()
      })
      box.prop("checked", main.profile.isChecked(id))
    })

    resolve();
  })

  mark = (id, on = null) => {
    if (on != null)
      $('#' + id).prop("checked", on);
    this.main.profile.toggleCheck(id, on);
    return $('#' + id).prop("checked")
  }

  markSingles = (id, on) => {
    let indexId = $("#" + id).data("fd--single-id")
    for (let a = 0; a < this.index[indexId].length; a++) {
      let boxId = this.index[indexId][a];
      this.mark(boxId, on)
    }
  }

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