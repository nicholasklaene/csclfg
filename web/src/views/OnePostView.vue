<script setup lang="ts">
import { onMounted, ref } from "vue";
import { useRoute } from "vue-router";
import { usePostStore } from "@/stores/post";
import { Post } from "@/types/post";
import AppNotFound from "@/components/AppNotFound.vue";

const route = useRoute();
const postStore = usePostStore();
const postId = String(route.params.id);

let post: Post;
let notFound = ref(false);

onMounted(async () => {
  if (!postStore.postCache.has(postId)) {
    await postStore.getPostById(postId);
  }

  if (!postStore.postCache.has(postId)) {
    notFound.value = true;
  }

  post = postStore.postCache.get(postId)!;
});
</script>

<template>
  <AppNotFound v-if="notFound" />
  <main v-else-if="!notFound && !postStore.loading">Post was found!</main>
</template>
