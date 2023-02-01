<template>
  <div class="component">


    <section class="container mt-3">
      <div class="row justify-content-center ">
        <div class="col-md-8 text-center rounded-5 p-5 text-shadow cover d-flex flex-column justify-content-between" :style="{ backgroundImage: `url(${vault?.img})` }">
          <h1>{{ vault?.name }}</h1>
          <p>by {{ vault?.creator?.name }}</p>
          <i class="mdi mdi-lock text-shadow fs-4" v-if="vault?.isPrivate"></i>
          <p class="fs-5">{{vault?.description}}</p>
        </div>
      </div>
      <div class="dropdown mt-5 d-flex" v-if="vault?.creator?.id == account?.id">
        <i class="mdi mdi-dots-horizontal fs-1 " type="button" data-bs-toggle="dropdown" aria-expanded="false"></i>
        <ul class="dropdown-menu ">
          <li data-bs-toggle="modal" data-bs-target="#edit-vault-modal" class="selectable">Edit Vault</li>
          <li @click="removeVault()" class="selectable">Delete Vault</li>
      
        </ul>
      </div>
      <div class="text-center rounded m-3" v-if="keeps.length > 0">
        
        <p class="fs-1">
          {{ keeps?.length }} Keeps
        </p>
      </div>
    </section>



    

    <section class="container">
      <div class="masonry my-5">
        <div v-for="k in keeps" :key="k.id">
          <KeepCard :keep="k" />
        </div>
      </div>
    </section>



  </div>
</template>


<script>
import { computed } from "@vue/reactivity";
import { onMounted} from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState.js";
import { router } from "../router.js";
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    const route = useRoute();
    async function getKeepsInVault() {
      try {
        await vaultsService.getKeepsInVault(route.params.id)
      } catch (error) {
        console.error('[keeps in vault]', error)
        Pop.error(error)
      }
    }
    async function setVaultActive() {
      try {
        await vaultsService.setVaultActive(route.params.id)
      } catch (error) {
        console.error('[]', error)
        Pop.error(error, "Private Vault, Stay Out")
        router.push({ name: 'Home' })
        AppState.activeVault = null
      }
    }


    onMounted(() => {
      getKeepsInVault()
      setVaultActive()
    })

    // watchEffect(() => {
    //   if (AppState.activeVault?.isPrivate) {
    //     if (AppState.activeVault.creator.id != AppState.account.id) {
    //       router.push({name: 'Home'})
    //     }
    //   }
    // })

    return {
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async removeVault() {
        try {
          if (await Pop.confirm())
            await vaultsService.removeVault(route.params.id)
        } catch (error) {
          console.error('[]', error)
          Pop.error(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.cover{
  background-size:cover;
  background-position: center;
// background-attachment: fixed;
height: 25rem;
object-fit: fill;
}

.masonry {
  columns: 4;
}

@media screen AND (max-width: 768px) {
  .masonry {
    columns: 2
  }
}
</style>