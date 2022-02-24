<script setup lang="ts">
import { useAuthStore } from "../stores/authStore";
import { useRoute } from "vue-router";
const currentRoute = useRoute();
const authStore = useAuthStore();
</script>

<template>
  <nav class="max-w-screen-lg mx-auto p-4">
    <ul class="flex text-white">
      <li>
        <router-link
          class="hover:underline"
          to="/"
          v-if="currentRoute.name !== 'Home'"
        >
          Home
        </router-link>
      </li>
      <li class="ml-auto">
        <template v-if="!authStore._isAuthenticated">
          <button
            @click="authStore.redirectToAuthServer(true)"
            class="border border-blue-300 bg-blue-300 hover:opacity-90 py-2 px-8 text-gray-900 mr-2"
          >
            Sign in
          </button>
          <button
            @click="authStore.redirectToAuthServer(false)"
            class="border border-blue-300 hover:opacity-90 py-2 px-8"
          >
            Sign up
          </button>
        </template>
        <template v-else>
          <button class="hover:underline" @click="authStore.logout()">
            Sign out
          </button>
        </template>
      </li>
    </ul>
  </nav>
</template>
