<template>
  <div class="card text-bg-light  border-0 my-3 elevation-5 selectable hover " :title="keep?.name"
    data-bs-toggle="modal" data-bs-target="#keep-modal" @click="getKeepDetails()">
    <i class="mdi mdi-close text-white bg-danger rounded-circle selectable on-hover text-center"
      v-if="keep.creator?.id == account?.id" title="Remove Keep"></i>
    <img :src="keep?.img" class="card-img" alt="...">
    <div class="card-img-overlay align-items-end d-flex justify-content-between">
      <h5 class="card-title text-shadow">{{
        keep?.name
      }}</h5>
      <img :src="keep.creator?.picture" class="person rounded-circle border border-white border-1" alt="" height="40"
        width="40" :title="keep.creator?.name" v-if="home">

    </div>
  </div>

</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted, ref, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js";
import { Keep } from "../models/Keep.js";
import { keepsService } from "../services/KeepsService.js";
import { vaultsService } from "../services/VaultsService.js";
import { routeLocationKey, useRoute } from "vue-router";
import { Modal } from "bootstrap";


export default {
  props: {
    keep: {
      type: Keep,
      required: true
    }
  },
  setup(props) {
    const editable = ref({});
    const route = useRoute()
    onMounted(() => {

    });
    watchEffect(() => { });

    return {
      editable,
      account: computed(() => AppState.account),
      home: computed(() => route.name == 'Home'),
      async getKeepDetails() {
        try {
          await keepsService.getKeepDetails(props.keep);
        } catch (error) {
          console.error('[]', error)
          Pop.error(error)
        }
      },

      async removeKeep() {
        try {
          if (await Pop.confirm()) {
            await keepsService.removeKeep(props.keep.id)
            Modal.getOrCreateInstance('#keep-modal').hide()
          }
        } catch (error) {
          console.error('[]', error)
          Pop.error(error)
        }
      },


      async getKeptKeepDetails() {
        try {
          await vaultsService.getKeptKeepDetails()
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
.card {
  position: relative;
}

i {
  position: absolute;
  right: 0;
  height: 20px;
  width: 20px;
  z-index: 9999;
}

.card-title {
  font-family: 'Marko One';
  font-style: normal;
  font-weight: 600;
  font-size: 22px;
  line-height: 29px;
}
</style>