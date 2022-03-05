<script setup lang="ts">
import { useAuthStore } from "../stores/authStore";

const authStore = useAuthStore();

const letter = authStore.username.charAt(0).toUpperCase();
</script>

<template>
  <router-link
    to="/profile"
    class="rounded-full text-3xl font-bold bg-blue-500 px-2 mt-1 ml-4 cursor-pointer"
  >
    {{ letter }}
  </router-link>
</template>
