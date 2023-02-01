<template>
  <section class="container">
    <div class="masonry my-5">
      <div v-for="k in keeps" :key="k.id">
        <KeepCard :keep="k" />
      </div>
    </div>
  </section>
</template>

<script>
import { onMounted } from "vue"
import { AppState } from "../AppState.js";
import KeepCard from "../components/KeepCard.vue";
import { keepsService } from '../services/KeepsService.js';
import { computed } from '@vue/reactivity';
import Pop from "../utils/Pop.js";
export default {
  setup() {
    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      }
      catch (error) {
        console.error("[]", error);
        Pop.error(error);
      }
    }
    onMounted(() => {
      getKeeps();
    });
    return {
      keeps: computed(() => AppState.keeps),
      activeKeep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account)
    };
  },
  components: { KeepCard }
}
</script>

<style scoped lang="scss">
.masonry {
  columns: 3;

}

@media screen AND (max-width: 768px) {

  .masonry {
    columns: 2
  }

}
</style>
