import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { KeptKeep } from "../models/KeptKeep.js";
import { Vault } from "../models/Vault.js";
import { VaultKeepIds } from "../models/VaultKeepIds.js";
import { router } from "../router.js";
import { api } from "./AxiosService.js"

class VaultsService {
  async createVault(data) {
    const res = await api.post("/api/vaults", data)
    console.log(res.data, 'res');
    AppState.myVaults.push(new Vault(res.data))
  }

  async getKeepsInVault(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    // console.log(res.data ,"keeps in vault");
    AppState.keeps = res.data.map(k => new Keep(k))


    // console.log(AppState.keeps);
  }

  async removeVault(id) {
    const res = await api.delete(`/api/vaults/${id}`)
    if (res) {
      router.push({ name: 'Account' })

    }
    // AppState.activeVault = AppState.activeVault.filter()
  }

  async setVaultActive(id) {
    const res = await api.get(`api/vaults/${id}`)
    // console.log(res.data);
    let vault = new Vault(res.data)
    // let userId = AppState.account.id
    // let notOwner = userId == vault.creator.id
    // if (vault.isPrivate && notOwner) {
    //   router.push({
    //     name: 'Home'
    //   })
    //   return
    // }
    AppState.activeVault = vault

    // console.log(AppState.activeVault);


  }

  async addToVault(vaultData) {
    // console.log(vaultData);
    const res = await api.post(`api/vaultkeeps`, vaultData)
    AppState.activeKeep.kept++
    // console.log(res.data);
    AppState.vaultKeepIds = [...AppState.vaultKeepIds, new VaultKeepIds(res.data)]
  }

  async editVault(vaultData, id) {
    const res = await api.put(`api/vaults/${id}`, vaultData)
    // console.log(res.data);
    AppState.activeVault = new Vault(res.data)
  }


  async removeVaultKeep(id) {
    const res = await api.delete(`api/vaultkeeps/${id}`)
    // console.log(res.data);

    AppState.keeps = AppState.keeps.filter(k => k.vaultKeepId != id)
  }

  async getAllVaultKeeps() {
    const res = await api.get("api/vaultkeeps")
    // console.log(res.data);
    AppState.vaultKeepIds = res.data.map(x => new VaultKeepIds(x))
    // console.log(AppState.vaultKeepIds);
  }
}

export const vaultsService = new VaultsService()