<script setup lang="ts">
import { useAuthStore } from "@/stores/auth";

const authStore = useAuthStore();

function signin() {
  authStore.redirectToAuthServer(true);
}
</script>

<template>
  <button class="btn btn-primary px-4 text-primaryText" @click="signin()">
    Sign in
  </button>
</template>
