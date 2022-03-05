<script setup lang="ts">
import { useAuthStore } from "../stores/authStore";
import { useRoute } from "vue-router";
import AppButton from "./buttons/AppButton.vue";
import AppOutlinedButton from "./buttons/AppOutlinedButton.vue";
import AppProfileIcon from "./AppProfileIcon.vue";
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
          <AppButton @click="authStore.redirectToAuthServer(true)">
            Sign in
          </AppButton>
          <AppOutlinedButton @click="authStore.redirectToAuthServer(false)">
            Sign up
          </AppOutlinedButton>
        </template>
        <template v-else>
          <AppOutlinedButton @click="authStore.logout()">
            Sign out
          </AppOutlinedButton>
        </template>
        <li v-if="authStore._isAuthenticated">
          <AppProfileIcon />
        </li>
      </li>
    </ul>
  </nav>
</template>
