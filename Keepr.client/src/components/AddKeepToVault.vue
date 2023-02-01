<template>
  <div class="component">
    <form @submit.prevent="addToVault()">
      
      <button class="btn btn-danger" type="submit">+</button>
    </form>

  </div>
</template>


<script>
import { computed } from "@vue/reactivity";
import { ref } from "vue"
import { AppState } from "../AppState.js";
import { vaultsService } from "../services/VaultsService.js"
import Pop from "../utils/Pop.js";

export default {
  setup() {
    const editable = ref({});
    return {
      editable,
      vaults: computed(() => AppState.vaults),
      async addToVault() {
        try {
          let data = {
              keepId: AppState.activeKeep.id, vaultId: editable.value.id
            }
          
          await vaultsService.addToVault(data)
        } catch (error) {
          console.error('[]', error)
          Pop
            .error(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>

</style>