<script setup lang="ts">
import AppNavbar from "./components/AppNavbar.vue";
import { useRoute } from "vue-router";
const route = useRoute();
</script>

<template>
  <AppNavbar v-if="route.name !== 'OAuthCallback'" />
  <router-view />
</template>

<style scoped>
main {
  max-width: 1024px;
}
</style>
