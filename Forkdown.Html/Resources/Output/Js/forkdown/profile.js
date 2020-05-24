'use strict'

class ForkdownProfile {
  /**
   * @param {Number} id
   * @param {String} name Must be [a-zA-Z_]
   */
  constructor({id, name, checked = {}} = {}) {
    this.id = id
    this.name = name
    this.checked = checked
  }


  /**
   * @param {String} checkboxId 
   * @param {Boolean} isChecked 
   */
  updateChecked(checkboxId, isChecked) {
    this.checked[checkboxId] = isChecked
  }



  static fromDto(dto) {
    return new ForkdownProfile({
      id: dto.id,
      name: dto.name,
      checked: dto.checked,
    })
  }

  toDto() {
    return {
      id: this.id,
      name: this.name,
      checked: this.checked
    }
  }
  
}