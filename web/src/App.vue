<script setup lang="ts">
import AppNavbar from "./components/AppNavbar.vue";
</script>

<template>
  <AppNavbar />
  <main class="mx-auto px-4">
    <router-view />
  </main>
</template>

<style scoped>
main {
  max-width: 1024px;
}
</style>
