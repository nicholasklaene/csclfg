<script setup lang="ts">
import AppOutlinedButton from "./AppOutlinedButton.vue";
import { useAuthStore } from "../../stores/authStore";
const authStore = useAuthStore();
</script>

<template>
  <AppOutlinedButton @click="authStore.logout()"> Sign out </AppOutlinedButton>
</template>
