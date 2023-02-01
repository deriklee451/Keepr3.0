import { AppState } from "../AppState.js";
import { Account } from "../models/Account.js";
import { Keep } from "../models/Keep.js";
import { Vault } from "../models/Vault.js";
import { api } from "./AxiosService.js"

class ProfilesService {
  async getProfile(id) {
    const res = await api.get(`api/profiles/${id}`)
    // console.log(res.data);
    AppState.activeProfile = res.data
    // console.log(AppState.activeProfile);
  }

  async getProfileKeeps(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    // console.log(res.data);
    // console.log(res.data);
    AppState.keeps = res.data.map(k => new Keep(k))
    // console.log(AppState.keeps);
  }

  async getProfileVaults(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    // console.log(res.data);
    // console.log(res.data);
    AppState.vaults = res.data.map(v => new Vault(v))
    // console.log(AppState.vaults);
  }
}


export const profilesService = new ProfilesService()