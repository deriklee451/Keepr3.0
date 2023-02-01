import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { Vault } from "../models/Vault.js"
import { Keep } from "../models/Keep.js"
import { Account } from "../models/Account.js"

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getAccountVaults() {
    const res = await api.get("account/vaults")
    // console.log(res.data);
    AppState.myVaults = res.data.map(v => new Vault(v))
    // console.log(AppState.vaults);
  }

  async getAccountKeeps() {
    const res = await api.get("account/keeps")
    // console.log(res.data);
    AppState.keeps = res.data.map(k => new Keep(k))
    // console.log(AppState.keeps);
  }

  async editAccount(formData) {
    const res = await api.put('/account', formData)
    AppState.account = new Account(res.data)
  }

}

export const accountService = new AccountService()