<script setup lang="ts">
import AppSigninButton from "./AppSigninButton.vue";
import AppSignupButton from "./AppSignupButton.vue";
import AppSignoutButton from "./AppSignoutButton.vue";
import { useRoute } from "vue-router";
import { useAuthStore } from "@/stores/auth";
const route = useRoute();
const authStore = useAuthStore();
</script>

<template>
  <nav
    class="navbar navbar-expand-lg navbar-dark sticky-top bg-backgroundCompliment py-3"
    role="navigation"
  >
    <div class="container-fluid mx-auto px-4">
      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarNav"
        aria-controls="navbarNav"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span class="navbar-toggler-icon"></span>
      </button>

      <div class="collapse navbar-collapse" id="navbarNav">
        <ul class="navbar-nav d-flex gap-3" id="links">
          <li
            v-if="route.name !== 'Home'"
            class="nav-item d-flex justify-content-center"
          >
            <router-link
              class="text-text text-decoration-none fw-bold"
              :to="{ name: 'Home' }"
            >
              Home
            </router-link>
          </li>
        </ul>
        <ul class="navbar-nav ms-auto d-flex gap-3">
          <template v-if="authStore.state.isAuthenticated">
            <li class="nav-item d-flex justify-content-center">
              <AppSignoutButton />
            </li>
          </template>
          <template v-else>
            <li class="nav-item d-flex justify-content-center">
              <AppSigninButton />
            </li>
            <li class="nav-item d-flex justify-content-center">
              <AppSignupButton />
            </li>
          </template>
        </ul>
      </div>
    </div>
  </nav>
</template>

<style scoped>
nav > div.container-fluid {
  max-width: 1024px;
}

@media screen and (max-width: 988px) {
  #links {
    margin-bottom: 1rem;
  }
}
</style>
