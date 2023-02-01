<template>
  <router-link :to="{ name: 'Vault', params: { id: vault.id } }">
    <div
      class="component hover my-2 mx-3 rounded-3 mt-4 elevation-3 hover d-flex justify-content-between align-items-end"
      :style="{ backgroundImage: `url(${vault.img})` }">
      <div class="d-flex justify-content-between">




        <div class="align-items-end d-flex text-light justify-content-between">

          <h1 class="text-shadow vault-name ms-4 mb-2">{{ vault.name }}</h1>


        </div>
      </div>
      <div>
        <i class="mdi mdi-lock text-shadow fs-2  rounded-circle" v-if="vault.isPrivate"></i>


      </div>
    </div>
  </router-link>

</template>


<script>
import { Vault } from "../models/Vault.js";




export default {
  props: {
    vault: {
      type: Vault,
      required: true
    }
  },
  setup(props) {
    return {

    }
  }
}
</script>


<style lang="scss" scoped>
.vault-name {
  font-family: 'Quando';
  font-style: normal;
  font-weight: 500;
  font-size: 24px;
  line-height: 30px;
}

.component {
  width: 350px;
  height: 250px;
  // object-fit: fill;
  background-size: cover;
  background-position: center;
}
</style>