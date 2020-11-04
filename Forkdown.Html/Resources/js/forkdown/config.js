class ForkdownConfig {
  /**
   * @param {String} projectName
   */
  constructor({projectName}) {
    if (!projectName)
      throw new Error("Cannot initialize project config without a project name!")
    this.projectName = projectName
  }
}