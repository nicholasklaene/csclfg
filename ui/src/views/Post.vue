<script setup lang="ts">
import { onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";
import { usePostStore } from "../stores/postStore";

const route = useRoute();
const router = useRouter();
const postStore = usePostStore();
const postId: string = String(route.params.id);

onMounted(async () => {
  if (!postStore.individualPosts.has(postId)) {
    await postStore.getPostById(postId);
  }

  if (!postStore.individualPosts.has(postId)) {
    router.push({ name: "404" });
  }
});
</script>

<template>
  <p class="text-white" v-if="!postStore.loading">
    {{ postStore.individualPosts.get(postId) }}
  </p>
</template>
