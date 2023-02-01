<template>
  <div class="modal-body p-0 elevation-5">
    <div class="row rounded-5">

      <div class="col-md-6 pic rounded-start" :style="{ backgroundImage: `url(${keep?.img})` }">

        <i class="mdi mdi-close text-danger fs-1 selectable rounded d-md-none d-block" title="close modal"
          data-bs-dismiss="modal"></i>
      </div>



      <div class="col-md-6 d-flex flex-column justify-content-between ">

        <div class="">

          <div class="text-center d-flex justify-content-between ">
            <div class="d-flex p-1">
            </div>

            <div class="d-flex gap-3 grey">
              <div class="d-flex">
                <i class="mdi mdi-eye fs-3"></i>
                <p class="mt-2">{{ keep?.views }} </p>
              </div>
              <div class="d-flex">
                <i class="mdi mdi-alpha-k-box fs-3"></i>
                <p class="mt-2">{{ keep?.kept }} </p>
              </div>
            </div>


            <div class=" me-3">
              <div v-if="keep.creator?.id == account?.id">
                <i class="mdi mdi-close  text-danger fs-3 selectable rounded-3" title="remove keep" v-if="routeHome"
                  @click="removeKeep()"></i>
              </div>

              <div v-if="user.isAuthenticated">
                <div v-if="vault?.creator?.id == account?.id">
                  <div class="dropdown ">
                    <i class="mdi mdi-dots-horizontal fs-1 " type="button" data-bs-toggle="dropdown"
                      aria-expanded="false"></i>
                    <ul class="dropdown-menu">
                      <li @click="removeVaultKeep(keep?.vaultKeepId)" class="selectable no-select">Remove From Vault
                      </li>

                    </ul>
                  </div>
                </div>
              </div>




            </div>
          </div>




        </div>




        <div class="">
          <h4 class="text-center keep-name">
            {{ keep?.name }}
          </h4>
          <p class="p-4 grey description">
            {{ keep?.description }}
          </p>
        </div>
        <div class="d-flex justify-content-end justify-content-between">
          <!-- Note Add keep to vault -->

          <form @submit.prevent="addToVault()" v-if="user.isAuthenticated">
            <div class="d-flex">
              <select class="form-select border-0" v-model="editable">
                <option v-for="v in vaults" :value="v"><a class="dropdown-item" placeholder="Add to Vault">
                    {{ v?.name }}
                  </a></option>
              </select>
              <button class="btn save border-0" type="submit">save</button>
            </div>
          </form>







          <div class="d-flex me-3 mb-1">
            <router-link :to="{ name: 'Profile', params: { id: keep.creator?.id } }">
              <img :src="keep.creator?.picture" alt="" class="rounded-circle mx-2 mb-2" height="40" width="40"
                data-bs-dismiss="modal">
            </router-link>
            <p class="name">{{ keep.creator?.name }}</p>
          </div>


        </div>
      </div>
    </div>
  </div>



</template>


<script>

import { computed } from "@vue/reactivity";
import { RouterLink, useRoute } from "vue-router";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import AddKeepToVault from "./AddKeepToVault.vue";
import { ref } from "vue"
import Pop from "../utils/Pop.js";
import { vaultsService } from "../services/VaultsService.js";
import { Modal } from "bootstrap";
import { keepsService } from "../services/KeepsService.js";

export default {
  props: {
    keep: {
      type: Keep,
      required: true
    }
  },
  setup() {
    const editable = ref({});
    const route = useRoute()
    return {
      editable,
      vaults: computed(() => AppState.myVaults),
      vault: computed(() => AppState.activeVault),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.activeKeep),
      routeAccount: computed(() => route.name.includes('Account')),
      routeVault: computed(() => route.name.includes('Vault')),
      routeHome: computed(() => route.name.includes('Home')),
      async addToVault() {
        try {
          let data = {
            keepId: AppState.activeKeep.id, vaultId: editable.value.id
          }

          await vaultsService.addToVault(data)
          Pop.success(`Added to your vault!`)
        } catch (error) {
          console.error('[]', error)
          Pop
            .error(error)
        }
      },

      async removeVaultKeep(id) {
        try {
          if (await Pop.confirm())
            await vaultsService.removeVaultKeep(id)
          Modal.getOrCreateInstance('#keep-modal').hide()
        } catch (error) {
          console.error('[]', error)
          Pop.error(error)
        }
      },

      async removeKeep() {
        try {
          if (await Pop.confirm()) {
            await keepsService.removeKeep(AppState.activeKeep.id)
            Modal.getOrCreateInstance('#keep-modal').hide()
          }
        } catch (error) {
          console.error('[]', error)
          Pop.error(error)
        }
      },
    };
  },
  components: { RouterLink, AddKeepToVault }
}
</script>


<style lang="scss" scoped>
.keep-name {
  font-family: 'Marko One';
  font-style: normal;
  font-weight: 400;
  font-size: 48px;
  line-height: 64px;
}

.description {
  font-family: 'Inter';
  font-style: normal;
  font-weight: 400;
  font-size: 18px;
  line-height: 175%;
}

.name {
  font-family: 'Oxygen';
  font-style: normal;
  font-weight: 700;
  font-size: 18px;
  line-height: 23px;
}

.pic {
  height: 70vh;
  background-size: cover;
  background-position: center;

}

option {
  font-family: 'Oxygen';
  font-style: normal;
  font-weight: 700;
  font-size: 20px;
  line-height: 25px;
}

select {
  font-family: 'Oxygen';
  font-style: normal;
  font-weight: 700;
  font-size: 20px;
  line-height: 25px;
}

.save {
  background-color: #A76BBD;
  color: white;

}

.piccy {
  object-fit: fill;
}
</style>