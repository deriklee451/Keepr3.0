<template>
  <div class="modal-body">
    <div class="row">
      <div class="col-md-6">
        <div class="d-flex justify-content-between">
          <h2 class="mb-5 ms-3 marko">Add your keep</h2>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <form @submit.prevent="createKeep()" class="d-flex flex-column justify-content-between ms-2 p-3">
          <div class="form mb-3">
            <input v-model="editable.name" required type="text" class="form-control" placeholder="Title...">

          </div>
          <div class="form mb-3">
            <textarea v-model="editable.description" required type="text" class="form-control"
              placeholder="Keep Description...">
          </textarea>

          </div>

          <input v-model="editable.img" type="url" class="form-control" placeholder="Image URL...">
          <div class="d-flex justify-content-end align-items-end mt-5">
            <button type="submit" class="btn btn-dark mt-5">Create</button>
          </div>
        </form>
      </div>



      <div class="col-md-6 input-image rounded d-flex text-shadow elevation-5"
        :style="{ backgroundImage: `url(${editable.img})` }">
        <div class="row">
          <p class="p-5">{{ editable.description }}</p>
          <div class="col-md-6  d-flex align-items-end">
            <h4 class="text-white fs-1">{{ editable.name }}</h4>
          </div>
        </div>
      </div>




    </div>
  </div>
</template>


<script>
import { Modal } from "bootstrap";
import { ref } from 'vue';
import { keepsService } from "../services/KeepsService.js";

import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createKeep() {
        try {
          await keepsService.createKeep(editable.value)
          Modal.getOrCreateInstance('#create-keep-modal').hide()
          Pop.success("Created a Keep!")
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.input-image {
  // object-fit: cover;
  background-size: cover;
  background-position: center;
  height: 40rem;
}
</style>