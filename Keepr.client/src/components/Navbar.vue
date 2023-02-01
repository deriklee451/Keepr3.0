<template>
  <nav class="navbar navbar-expand-lg   px-3 d-flex justify-content-between elevation-3 ">
    <div class="d-flex">
      <router-link class="" :to="{ name: 'Home' }">
        <button class="button btn border border-dark border-2 marko me-3 ">Home</button>
      </router-link>

      <div v-if="user.isAuthenticated" class="">
        <div class="dropdown" v-if="!profilePage">
          <button
            class="btn btn border border-dark border-2 dropdown-toggle dropdown-toggle-split marko justify-content-end"
            type="button" data-bs-toggle="dropdown" aria-expanded="false">
            Create
          </button>
          <ul class="dropdown-menu text-center p-0">
            <li data-bs-toggle="modal" data-bs-target="#create-keep-modal" class="selectable marko">new keep</li>
            <li>
              <hr class="dropdown-divider">
            </li>
            <li data-bs-toggle="modal" data-bs-target="#create-vault-modal" class="selectable marko">new vault</li>
          </ul>
        </div>
      </div>
    </div>




    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <img alt="logo" src="src\assets\img\Keepr logo.png" class="m-auto" height="60" />
      </div>
    </router-link>







    <Login class="hover" />


  </nav>
</template>

<script>
import Login from './Login.vue'
import { useRoute } from "vue-router";
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";
export default {
  setup() {
    const route = useRoute()
    return {
      profilePage: computed(() => route.name == "Profile"),
      user: computed(() => AppState.user),
    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

nav {
  background-color: #FEF6F0;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
  background: #FEF6F0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
