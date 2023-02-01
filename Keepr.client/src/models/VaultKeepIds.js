import { Account } from "./Account.js"

export class VaultKeepIds {
  constructor(data) {
    this.keepId = data.keepId


    this.vaultId = data.vaultId
    this.vaultKeepId = data.id

    this.creatorId = data.creatorId

  }
}
