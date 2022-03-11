<script setup lang="ts">
import { onMounted, ref } from "vue";
import { useRoute } from "vue-router";
import { marked } from "marked";
import { usePostStore } from "@/stores/post";
import { Post } from "@/types/post";
import AppNotFound from "@/components/AppNotFound.vue";

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
  <div v-if="!postStore.loading">
    <AppNotFound v-if="!found" />
    <main v-else>
      <div v-html="marked.parse(post!.description)"></div>
    </main>
  </div>
</template>
