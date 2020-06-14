'use strict'

class ForkdownChecklists {
  /**
   * 
   * @param {HTMLElement} container 
   */
  static totals(container) {
    let total = $(".fd--checkbox > input[type='checkbox']")
    let checked = ("*[checked]", total);
    console.log(checked, "/", total);
  }
}