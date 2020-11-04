'use strict'

class ForkdownProfile {
  /**
   * @param {Number} id
   * @param {String} name
   * @param {{String: ForkdownProfileItem}} items
   * @param {ForkdownStorage} storage
   */
  constructor({id, name, items = {}} = {}) {
    this.id = id
    this.name = name
    this.items = items
  }


  isChecked = (id) => {
    return this.items[id] && this.items[id].isChecked
  }

  /**
   *
   * @param {String} id
   * @param {Boolean} check
   */
  toggleCheck = (id, check) => {
    let item = this.items[id] || new ForkdownProfileItem({id})
    item.isChecked = check
    this.items[id] = item
  }
}