class ForkdownProfileItem {
  /**
   *
   * @param {String} id
   * @param {Boolean} isChecked
   */
  constructor({id, isChecked = false}) {
    this.id = id
    this.isChecked = isChecked
  }
}