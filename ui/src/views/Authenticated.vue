<script setup lang="ts">
import { useAuthStore } from "../stores/authStore";
const authStore = useAuthStore();
</script>

<template>
  <p>You are authenticated!</p>
  <p>Email: {{ authStore.email }}</p>
</template>
