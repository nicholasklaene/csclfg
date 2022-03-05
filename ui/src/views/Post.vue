<script setup lang="ts">
import { useRoute } from "vue-router";
const route = useRoute();
const postId = route.params.id;
console.log(postId);
</script>

<template>
  <h1>wow</h1>
</template>
