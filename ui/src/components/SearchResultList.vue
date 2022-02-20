<script setup lang="ts">
import { usePostStore } from "../stores/postStore";
import SearchResult from "./SearchResult.vue";

const postStore = usePostStore();
</script>

<template>
  <section class="flex flex-col">
    <SearchResult
      v-for="post in postStore.posts"
      :key="post.post_id"
      :post="post"
    />
  </section>
</template>
