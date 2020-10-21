'use strict'

class ForkdownItems {
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
    // Singles
    this.index = $("#fd--singles-index").data("index")
    if (this.index.length < 1)
      throw new Error("No singles index!")

    for (const key in this.index) {
      this.index[key].forEach(id => {
        $(document.getElementById(id)).data("fd--single-id", key)
      })
    }

    // CHECKBOXES
    $(".fd--checkbox > input").each((i, c) => {
      let box = $(c)
      let id = box.prop("id")
      if (!id) {
        console.log(c)
        throw new Error("Checkbox without an ID")
      }

      box.on("change", async () => {
        let checked = this.mark(id)
        if (box.data("fd--single-id"))
          this.markSingles(id, checked);
        await this.main.saveProfile()
      })
      box.prop("checked", this.main.profile.isChecked(id))
    })

    resolve();
  })

  mark = (id, on = null) => {
    let box = document.getElementById(id)
    if (box) {
      if (on != null) {
        console.log("Marking " + box.id + " as checked: ", on)
        $(box).prop("checked", on);
      }
      on = $(box).prop("checked") === true
    }
    console.log("Saving " + id + " as checked: ", on)
    this.main.profile.toggleCheck(id, on);
    return on
  }

  markSingles = (id, on) => {
    let box = document.getElementById(id)
    let indexId = $(box).data("fd--single-id")
    for (let a = 0; a < this.index[indexId].length; a++) {
      let boxId = this.index[indexId][a];
      if (boxId === box.id)
        continue
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