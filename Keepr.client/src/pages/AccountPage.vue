<template>
  <div class="container pt-4">





    <div class="col-8 about text-center cover container rounded-5 "
      :style="{ backgroundImage: `url(${account.coverImg})` }">
      <div class=" info mb-5">
        <img class="rounded-circle border border-white border-4" height="100" width="150" :src="account.picture"
          alt="" />
        <p class="name">{{ account.name }}</p>
        <p class="fs-1">
          {{ vaults.length }} Vaults | {{ keeps.length }} Keeps
        </p>
      </div>

    </div>




    <div class="container-fluid">

      <div class="dropdown mt-5">
        <i class="mdi mdi-dots-horizontal fs-1 " type="button" data-bs-toggle="dropdown" aria-expanded="false"></i>
        <ul class="dropdown-menu">
          <li data-bs-toggle="modal" data-bs-target="#edit-account-modal">Edit Account</li>
        </ul>
      </div>


      <h3>Vaults</h3>
      <section class="container vaults mt-5">
        <div class="d-flex">
          <div v-for="v in vaults" :key="v.id">
            <VaultCard :vault="v" />
          </div>
        </div>
      </section>
    </div>



    <h3 class="mt-5">Keeps</h3>
    <section class="container mt-5">
      <div class="masonry my-5">
        <div v-for="k in keeps" :key="k.id">
          <KeepCard :keep="k" />
        </div>
      </div>
    </section>
  </div>



</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import VaultCard from "../components/VaultCard.vue"
import { accountService } from "../services/AccountService.js"
import Pop from "../utils/Pop.js"
export default {
  setup() {
    async function getAccountKeeps() {
      try {
        await accountService.getAccountKeeps();
      }
      catch (error) {
        console.error("[]", error);
        Pop.error(error);
      }
    }
    async function getAccountVaults() {
      try {
        await accountService.getAccountVaults();
      }
      catch (error) {
        console.error("[]", error);
        Pop.error(error);
      }
    }
    onMounted(() => {
      getAccountKeeps();
      getAccountVaults();
    });
    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.myVaults)
    };
  },
  components: { VaultCard }
}
</script>

<style scoped>
.cover {

  background-size: cover;
  height: 25rem;
  background-position: center;
  position: relative;
}

.name {
  font-weight: 400;
}

img {
  max-width: 100px;
}

.vaults {
  width: 80vw;
  overflow-y: hidden;
}

.masonry {
  columns: 4;
}

.info {
  position: absolute;
  margin-top: 20rem;
  margin-left: 16rem;
  font-family: 'Oxygen';
  font-style: normal;
  /* font-weight: 700; */
  font-size: 48px;
  line-height: 61px;
}

@media screen AND (max-width: 768px) {
  .masonry {
    columns: 2
  }

  .info {
    margin-top: 20rem;
    margin-left: 0;
  }
}
</style>
