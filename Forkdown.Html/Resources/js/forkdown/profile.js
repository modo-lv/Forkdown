'use strict'

class ForkdownProfile {
  /**
   * @param {Number} id
   * @param {String} name Must be [a-zA-Z_]
   * @param elements
   */
  constructor({id, name, elements: elements = {}} = {}) {
    this.id = id
    this.name = name
    this.elements = elements
  }


  /**
   * @param {String} elementId
   * @param {Boolean} isChecked
   */
  toggleCheck(elementId, isChecked) {
    this.checkItem(elementId).isChecked = (isChecked === true)
  }

  checkItem(elementId) {
    let el = this.element(elementId)
    return el.checkItem = el.checkItem ?? {}
  }

  element(elementId) {
    return this.elements[elementId] = this.elements[elementId] ?? {}
  }

  /**
   * Get the checked status of a given element.
   * @param {String} elementId
   */
  isChecked(elementId) {
    return this.checkItem(elementId).isChecked === true
  }


  static fromDto(dto) {
    let profile = new ForkdownProfile({
      id: dto.id,
      name: dto.name,
      elements: dto.elements,
    })

    if (dto.checked != null) {
      for (const id in dto.checked) {
        // noinspection JSUnfilteredForInLoop
        profile.toggleCheck(id, dto.checked[id])
      }
    }

    return profile
  }

  toDto() {
    return {
      id: this.id,
      name: this.name,
      elements: this.elements
    }
  }

}