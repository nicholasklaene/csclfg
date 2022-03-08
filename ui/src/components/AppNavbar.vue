<script setup lang="ts">
import { useAuthStore } from "../stores/authStore";
import { useRoute } from "vue-router";
import AppProfileIcon from "./AppProfileIcon.vue";
import AppSigninButton from "./buttons/AppSigninButton.vue";
import AppSignupButton from "./buttons/AppSignupButton.vue";
import AppSignoutButton from "./buttons/AppSignoutButton.vue";
const currentRoute = useRoute();
const authStore = useAuthStore();
</script>

<template>
  <nav class="max-w-screen-lg mx-auto p-4">
    <ul class="flex text-white">
      <li>
        <router-link
          class="hover:underline font-bold tracking-wide"
          to="/"
          v-if="currentRoute.name !== 'Home'"
        >
          Home
        </router-link>
      </li>
      <li class="ml-auto flex gap-2">
        <template v-if="!authStore._isAuthenticated">
          <AppSigninButton />
          <AppSignupButton />
        </template>
        <template v-else>
          <AppSignoutButton />
        </template>
        <li v-if="authStore._isAuthenticated">
          <AppProfileIcon />
        </li>
      </li>
    </ul>
  </nav>
</template>
