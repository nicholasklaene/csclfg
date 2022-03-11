<script setup lang="ts">
import { useAuthStore } from "@/stores/auth";
const authStore = useAuthStore();
</script>

<template>
  <button class="btn btn-outline-primary px-4" @click="authStore.logout()">
    Sign out
  </button>
</template>
