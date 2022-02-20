<script setup lang="ts">
import { onMounted } from "vue";
import { usePostStore } from "../stores/postStore";

const postStore = usePostStore();

onMounted(() => {
  postStore.getPosts();
});
</script>

<template>
  <div class="text-4xl text-white">Hello, home!</div>
</template>
