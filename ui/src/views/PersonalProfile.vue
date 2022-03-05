<script setup lang="ts">
import { useAuthStore } from "../stores/authStore";
const authStore = useAuthStore();
</script>

<template>
  <div>{{ authStore.username }}</div>
</template>
