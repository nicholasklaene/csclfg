<script setup lang="ts">
import AppButton from "./AppButton.vue";
import { useAuthStore } from "../../stores/authStore";
const authStore = useAuthStore();
</script>

<template>
  <AppButton @click="authStore.redirectToAuthServer(true)">Sign in</AppButton>
</template>
