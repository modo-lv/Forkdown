'use strict'

class ForkdownProfile {
  /**
   * @param {Number} id
   * @param {String} name
   * @param {{String: ForkdownProfileItem}} items
   * @param {ForkdownProfileSet} profileSet
   * @param {{id: String, position: Number}} scrollPosition
   */
  constructor({
    id = 0,
    name,
    items = {},
    scrollPosition = {}
  }, profileSet) {
    if (!(profileSet instanceof ForkdownProfileSet)) {
      console.error("Invalid `profileSet`:", profileSet)
      throw new Error("Profile object instantiation failed.")
    }

    this.id = id
    this.name = name
    this.items = items
    this.scrollPosition = scrollPosition
    /** @type {function: ForkdownProfileSet} **/
    this.profiles = () => profileSet
  }

  storageKey = () => `profile[${this.id}]`

  save = async () => this.profiles().saveProfile(this)

  /**
   *
   * @param {String} id
   * @return {Boolean}
   */
  isChecked = (id) => {
    return this.items[id] && this.items[id].isChecked
  }

  /**
   *
   * @param {String} id
   * @param {Boolean} check
   */
  toggleCheck = (id, check) => {
    let item = this.items[id] || new ForkdownProfileItem({ id })
    item.isChecked = check
    this.items[id] = item
  }

  /**
   *
   * @param {String} id
   * @param {Number} position
   */
  saveScrollPosition = async (id, position) => {
    this.scrollPosition = { id, position }
    return this.save()
  }
}