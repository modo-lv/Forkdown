class ForkdownStorage {
  /**
   * @param localForage Instance of `localForage` to use as the underlying mechanism.
   * @param {String} dbName Name to use for storage database. Will be prefixed by "forkdown:", should be project name.
   * @param {String} version
   */
  constructor({localForage, dbName, version} = {}) {
    this.lf = localForage
    this.db = null
    this.dbName = `forkdown:${dbName}`
    this.version = version
  }

  /**
   *
   * @return {Promise<void>}
   */
  init = async () => {
    this.db = this.lf.createInstance({
      name: this.dbName,
      storeName: `v${this.version}`
    })
  }

  /**
   *
   * @param {ForkdownProfile} profile
   * @return {Promise<any>}
   */
  saveProfile = async (profile) => {
    this.db.setItem(`profile[${profile.id}]`, profile)
  }


  /**
   *
   * @param {RegExp} match
   * @return {Promise<any>}
   */
  keys = async (match= /./) =>
    this.db.keys().then(keys => keys.filter(key => key.match(match)))

  /**
   * @param {String} key
   * @returns {Promise<any>}
   */
  getItem = async (key) => this.db.getItem(key)

  /**
   *
   * @param {Array.<String>} keys
   * @return {Promise<Array.<any>>}
   */
  getItems = async (keys) => {
    return Promise.all(keys.map(key => this.db.getItem(key)))
  }

  /**
   * @param {String} key
   * @param {any} value
   * @returns {Promise<*>}
   */
  setItem = async (key, value) => this.db.setItem(key, JSON.parse(JSON.stringify(value)))

  /**
   *
   * @param {{String: *}} items
   * @return {Promise<*>}
   */
  setItems = async (items) => {
    let saves = []
    for (const key in items) {
      saves.push(this.setItem(key, items[key]))
    }
    return Promise.allSettled(saves)
  }
}