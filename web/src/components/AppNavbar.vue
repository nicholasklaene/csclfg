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
    class="navbar navbar-expand navbar-dark sticky-top bg-backgroundCompliment py-3"
  >
    <div class="container-fluid mx-auto px-4">
      <div class="navbar-collapse">
        <ul class="navbar-nav">
          <li v-if="route.name !== 'Home'" class="nav-item me-4">
            <router-link
              class="text-text text-decoration-none fw-bold"
              :to="{ name: 'Home' }"
            >
              Home
            </router-link>
          </li>
          <li class="nav-item me-4">
            <a
              href="https://studyseeking.com/about"
              target="__blank"
              class="text-text text-decoration-none fw-bold"
            >
              About
            </a>
          </li>
          <li class="nav-item">
            <a
              href="https://studyseeking.com/"
              target="__blank"
              class="text-text text-decoration-none fw-bold"
            >
              Study Apps
            </a>
          </li>
        </ul>
        <ul class="navbar-nav ms-auto">
          <template v-if="authStore.state.isAuthenticated">
            <li class="nav-item">
              <AppSignoutButton />
            </li>
          </template>
          <template v-else>
            <li class="nav-item me-2">
              <AppSigninButton />
            </li>
            <li class="nav-item">
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
</style>
