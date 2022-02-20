<script setup lang="ts">
import { usePostStore } from "../stores/postStore";
import SearchResult from "./SearchResult.vue";

const postStore = usePostStore();
</script>

<template>
  <section class="flex flex-col">
    <div
      v-for="post in postStore.posts"
      :key="post.post_id"
      class="border-b last-of-type:border-0 border-collapse border-gray-500 px-4 py-6"
    >
      <SearchResult :post="post" />
    </div>
  </section>
</template>
