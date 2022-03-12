<script setup lang="ts">
import { onMounted, ref } from "vue";
import { useRoute } from "vue-router";
import { usePostStore } from "@/stores/post";
import { Post } from "@/types/post";
import AppNotFound from "@/components/AppNotFound.vue";
import OnePostPost from "@/components/OnePostPost.vue";
import AppProgressBar from "@/components/AppProgressBar.vue";

const route = useRoute();
const postStore = usePostStore();
const postId = String(route.params.id);

let post = ref<Post | undefined>();
let found = ref(false);

onMounted(async () => {
  if (!postStore.postCache.has(postId)) {
    await postStore.getPostById(postId);
  }

  if (!postStore.postCache.has(postId)) {
    found.value = false;
  }

  post.value = postStore.postCache.get(postId)!;
  found.value = true;
});
</script>

<template>
  <AppProgressBar v-if="postStore.loading" />
  <div class="mx-auto mb-5">
    <main class="px-4 mt-4" v-if="!postStore.loading">
      <AppNotFound v-if="!found" />
      <OnePostPost v-else :post="post!" />
    </main>
  </div>
</template>
